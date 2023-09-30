using Assets.Scripts.Entities.Player;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.UI.Menu.MainMenu.Level;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class KillAllMobsChain : TrainingActionChain
	{
		protected override void OnConfigure(Player player)
		{
			StageController.Singleton.DisableAutoactivateWave = true;
			StageController.Singleton.OnStageCleanedUp += OnCleaned;
		}
		private void OnCleaned()
		{
			LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.NONE);
			StageController.Singleton.OnStageCleanedUp -= OnCleaned;
			ConfirmChain();
		}
	}
}
