using Assets.Scripts.Entities.Player;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Training.ActionChain.Chains
{
	internal class MoveDockspaceChain : TrainingActionChain
	{
		protected override void OnConfigure(Player player)
		{
			Editor.Editor.Instance._levelActions.ActivateButton(Editor.ButtonType.MOVE_NEXT_LEVEL);
			StageController.Singleton.OnDockspaceMoved += OnDockspaceMoved;
		}
		private void OnDockspaceMoved()
		{
			StageController.Singleton.OnDockspaceMoved -= OnDockspaceMoved;
			ConfirmChain();
		}
	}
}
