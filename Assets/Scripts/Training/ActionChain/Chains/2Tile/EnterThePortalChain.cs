using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Structures.Portal;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
    internal class EnterThePortalChain : TrainingActionChain
    {
        private void Awake()
        {
            FindObjectOfType<EndLevelPortal>().OnActivateAction = () => SceneManager.LoadScene(0);
        }
        protected override void OnConfigure(Player player)
        {

        }
    }
}