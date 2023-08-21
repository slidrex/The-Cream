using Assets.Scripts.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Advanced
{
    internal class AdvancedSettingsView : MonoBehaviour
    {
        [UnityEngine.SerializeField] private Toggle _enableQuickcast;
        private void Start()
        {
            _enableQuickcast.SetIsOnWithoutNotify(ConfigManager.Instance.SettingsConfig.IsQuickcastEnabled);
            _enableQuickcast.onValueChanged.AddListener(OnQuickcastToggleChanged);
        }
        private void OnQuickcastToggleChanged(bool value)
        {
            ConfigManager.Instance.SettingsConfig.IsQuickcastEnabled = value;
        }
    }
}
