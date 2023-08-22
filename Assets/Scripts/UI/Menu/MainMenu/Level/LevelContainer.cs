using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.MainMenu.Level
{
    internal class LevelContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _levels, _characters;
        [SerializeField] private Menus.MainMenu _menu;
        [SerializeField] private LevelHolder[] _holders;
        private void Awake()
        {
            for(int i = 0; i < _holders.Length; i++)
            {
                _holders[i].Configure(_levels, _characters, _menu, i + 1, i + 1);
            }
        }
    }
}
