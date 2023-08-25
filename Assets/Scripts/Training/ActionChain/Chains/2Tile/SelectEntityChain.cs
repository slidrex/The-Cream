using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Training.ActionChain.Chains
{
	internal class SelectEntityChain : TrainingActionChain
	{
		[SerializeField] private GameObject _editorPlace;
		protected override void OnConfigure(Player player)
		{
			TrainingCompositeRoot.Instance.HighlightController.HighlightElements(false, _editorPlace);
			Editor.Editor.Instance._editSystem.OnEntityPlaced += OnPlace;
		}
		private void OnPlace()
		{
			TrainingCompositeRoot.Instance.HighlightController.UnhighlightElements();
			ConfirmChain();
			Editor.Editor.Instance._editSystem.OnEntityPlaced -= OnPlace;
		}
	}
}
