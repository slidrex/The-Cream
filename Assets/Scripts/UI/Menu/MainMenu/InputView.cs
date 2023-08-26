using Assets.Scripts.Config;
using Assets.Scripts.Entities.Util.Config.Input;
using Assets.Scripts.Entities.Util.Config.Menu;
using System;
using UnityEngine;

namespace Assets.Scripts.Menu.MainMenu
{
    [Serializable]
    internal struct InputAction
    {
        public InputConfig.ActionKey ActionKey;
    }
    internal class InputView : MonoBehaviour
    {
        [SerializeField] private Transform сonfigurationContainer;
        [SerializeField] private InputAction[] _actions;
        [SerializeField] private KeyConfiguration[] configurations;
        private InputListener _listener;
        private InputConfig.ActionKey _currentKey;

        private void Start()
        {
            configurations = сonfigurationContainer.GetComponentsInChildren<KeyConfiguration>();
            _listener = GetComponent<InputListener>();
            for(int i = 0; i < configurations.Length; i++)
            {
                configurations[i].SetActionKey(_actions[i].ActionKey);
                configurations[i].SetListener(_listener);
                configurations[i].SetInputView(this);
            }
            
            UpdateConfigurations();
        }
        public void UpdateConfigurations()
        {
            foreach (var item in configurations)
            {
                UpdateKeys(item);
            }
        }
        public void UpdateKeys(KeyConfiguration config)
        {
            foreach (var key in ConfigManager.Instance.InputConfig.Keys)
            {
                if (key.Key == config.ActionKey && key.Value != config.KeyCode)
                {
                    config.SetKeyName(key.Value);
                }
            }
        }
    }
}
