using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class StartBattleChain : TrainingActionChain
	{
		[UnityEngine.SerializeField] private GameObject _buttonObject;
		protected override void OnConfigure(Player player)
		{

			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnLevelModeChanged;
			Editor.Editor.Instance._spaceController.OnSpaceChanged += OnSpaceChanged;
		}
		private void OnSpaceChanged(int newSpace)
		{
			if(newSpace < Editor.Editor.Instance._spaceController.GetMaxSpaceReqiured())
			{
				Editor.Editor.Instance._spaceController.OnSpaceChanged -= OnSpaceChanged;
				LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnLevelModeChanged;
				MoveToPreviousChain();
			}
		}
		private void OnLevelModeChanged(GameMode mode)
		{
			if (mode != GameMode.RUNTIME) throw new Exception("Unknown behaviour");
			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnLevelModeChanged;
			Editor.Editor.Instance._spaceController.OnSpaceChanged -= OnSpaceChanged;
			ConfirmChain();
		}
	}
}
