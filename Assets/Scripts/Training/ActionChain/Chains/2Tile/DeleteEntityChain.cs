using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Training.ActionChain.Chains
{
	internal class DeleteEntityChain : TrainingActionChain
	{
		protected override void OnConfigure(Player player)
		{
			var placedMob = FindObjectOfType<Mob>();
			print(placedMob);
			TrainingCompositeRoot.Instance.HighlightController.HighlightEntities(new List<SpriteRenderer>() { placedMob.SpriteRenderer });
			Editor.Editor.Instance._editSystem.OnEntityDeleted += OnDelete;
			Editor.Editor.Instance._editSystem.AllowPlacing = false;
		}
		private void OnDelete()
		{
			ConfirmChain();
			TrainingCompositeRoot.Instance.HighlightController.UnhighlightEntities();
			Editor.Editor.Instance._editSystem.AllowPlacing = true;
			Editor.Editor.Instance._editSystem.OnEntityDeleted -= OnDelete;
		}
	}
}
