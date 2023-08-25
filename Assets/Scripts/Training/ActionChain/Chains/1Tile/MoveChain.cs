using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Moving;
using Assets.Scripts.Training.Highlight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Training.ActionChain.Chains
{
	internal class MoveChain : TrainingActionChain
	{
		private PlayerMovement _movement;
		private int _movedCount;
		[UnityEngine.SerializeField] private int MOVE_COUNT_TO_NEXT_STEP = 20;

		protected override void OnConfigure(Player player)
		{
			_movement = player.GetComponent<PlayerMovement>();
			_movement.OnMoveTargetSelect += OnPlayerMoved;
			TrainingCompositeRoot.Instance.HighlightController.HighlightEntities(new List<SpriteRenderer>() { player.SpriteRenderer });
		}
		private void OnPlayerMoved(PlayerMovement.TargetType type)
		{
			if(type == PlayerMovement.TargetType.POINT)
			{
				if (_movedCount == 0) TrainingCompositeRoot.Instance.HighlightController.UnhighlightEntities();

				_movedCount++;
			}
			if (_movedCount >= MOVE_COUNT_TO_NEXT_STEP)
			{
				_movement.OnMoveTargetSelect -= OnPlayerMoved;
				ConfirmChain();
			}
		}
	}
}
