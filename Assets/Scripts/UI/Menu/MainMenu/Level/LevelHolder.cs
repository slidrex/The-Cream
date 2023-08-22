using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu.MainMenu.Level
{
    internal class LevelHolder : MonoBehaviour
    {
        public int Level { get; private set; }
        private Button _button;
        private GameObject _characters;
        private GameObject _levels;
        private Menus.MainMenu _menu;
        private int _sceneId;
        public void Configure(GameObject levels, GameObject characters, Menus.MainMenu menu, int displayLevel, int sceneId)
        {
            _sceneId = sceneId;
            Level = displayLevel;
            _menu = menu;
            _characters = characters;
            _levels = levels;
        }
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonPress);
        }
        public void OnButtonPress()
        {
            _levels.SetActive(false);
            _characters.SetActive(true);
            _menu.SceneID(_sceneId);
        }
    }
}
