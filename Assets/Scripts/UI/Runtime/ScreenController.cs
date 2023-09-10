using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Runtime
{
    internal class ScreenController : MonoBehaviour
    {
        [SerializeField] private Image _fade;
        [SerializeField] private ScreenObject[] _screens;
        private Dictionary<Screen, UIScreen> _screensDict;
        private Screen _activeScreen;
        public static ScreenController Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
        [Serializable]
        public struct ScreenObject
        {
            public Screen Screen;
            public UIScreen ScreenGameObject;
        }
        public enum Screen
        {
            NONE,
            DEATH,
            PAUSE
        }
        private void Start()
        {
            Configure();
        }
        public void EnableScreen(Screen screen)
        {
            _activeScreen = screen;
            var scr = _screensDict[screen];
            _fade.gameObject.SetActive(true);
            scr.gameObject.SetActive(true);
            scr.OnScreenEnable();
        }
        public void DisableScreen(Screen screen)
        {
            _fade.gameObject.SetActive(false);
            if (screen != _activeScreen) throw new Exception("Active screen and requested screen do not match.");
            var scr = _screensDict[screen];
            scr.gameObject.SetActive(false);

            _activeScreen = Screen.NONE;
        }
        private void Configure()
        {
            _screensDict = new();
            foreach(var dict in _screens)
            {
                _screensDict.Add(dict.Screen, dict.ScreenGameObject);
            }
        }
    }
}
