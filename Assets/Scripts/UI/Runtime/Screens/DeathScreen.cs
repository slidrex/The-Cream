using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UI;
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
            _menuButton.onClick.AddListener(() => SceneManager.LoadScene(MENU_BUILD_INDEX));
            _retryButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }
}
