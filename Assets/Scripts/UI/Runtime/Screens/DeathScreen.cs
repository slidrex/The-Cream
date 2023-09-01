using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Runtime.Screens
{
    internal class DeathScreen : UIScreen
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _retryButton;
        private const int MENU_BUILD_INDEX = 0;
        private void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
        }
        private void OnMenuButtonClicked()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(MENU_BUILD_INDEX);
        }
        private void OnRetryButtonClicked()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
