using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Structures.Portal;
using Assets.Scripts.GameProgress;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class EnterThePortalChain : TrainingActionChain
	{
		private void Awake()
		{
			PersistentData.IsTutorialPassed = true;
			FindObjectOfType<EndLevelPortal>().OnActivateAction = () => SceneManager.LoadScene(0);
		}
		protected override void OnConfigure(Player player)
		{

		}
	}
}
