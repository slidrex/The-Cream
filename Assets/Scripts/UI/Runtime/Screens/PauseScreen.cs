using EventStates;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Runtime.Screens
{
    internal class PauseScreen : UIScreen
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _resumeButton;
        private const int MENU_BUILD_INDEX = 0;
        private void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
            _resumeButton.onClick.AddListener(OnResume);
        }

        public override void OnScreenEnable()
        {
            PauseController.PauseGame();
        }

        private void OnResume()
        {
            PauseController.UnpauseGame();
            ScreenController.Instance.DisableScreen(ScreenController.Screen.PAUSE);
        }
        private void OnMenuButtonClicked()
        {
            PauseController.UnpauseGame();
            SceneManager.LoadScene(MENU_BUILD_INDEX);
        }
        private void OnRetryButtonClicked()
        {
            PauseController.UnpauseGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
