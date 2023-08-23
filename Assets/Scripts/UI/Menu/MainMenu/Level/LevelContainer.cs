using Assets.Scripts.GameProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Assets.Scripts.UI.Menu.MainMenu.Level
{
    internal class LevelContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _levels, _characters;
        [SerializeField] private Menus.MainMenu _menu;
        [SerializeField] private LevelHolder[] _holders;
        private void Start()
        {
            for(int i = 0; i < _holders.Length; i++)
            {
                var holder = _holders[i];
                int displayLevel = i + 1;
                holder.Configure(_levels, _characters, _menu, displayLevel, displayLevel);
                holder.SetLockStatus(PersistentData.CurrentGameLevel < displayLevel);
                holder.GetComponent<LocalizeStringEvent>().RefreshString();
            }
        }
    }
}
