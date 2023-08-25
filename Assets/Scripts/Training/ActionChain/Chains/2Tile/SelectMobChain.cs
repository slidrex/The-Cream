using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Moving;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class SelectMobChain : TrainingActionChain
	{
		private PlayerMovement _movement;
		protected override void OnConfigure(Player player)
		{
			TrainingCompositeRoot.Instance.HighlightController.HighlightEntities(FindObjectsOfType<Entity>().Select(x => x.SpriteRenderer));
			Time.timeScale = 0;
			_movement = player.GetComponent<PlayerMovement>();
			_movement.OnMoveTargetSelect += OnPlayerMoved;
		}
		private void OnPlayerMoved(PlayerMovement.TargetType type)
		{
			if(type == PlayerMovement.TargetType.ENEMY)
			{
				Time.timeScale = 1;
				TrainingCompositeRoot.Instance.HighlightController.UnhighlightEntities();
				_movement.OnMoveTargetSelect -= OnPlayerMoved;
				ConfirmChain();
			}
		}
	}
}
