using Assets.Scripts.Entities.Util.Config.Input;
using Assets.Scripts.Entities.Util.Config.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Menu.MainMenu
{
    [Serializable]
    internal struct InputAction
    {
        public string ActionName;
        public InputConfig.ActionKey ActionKey;
    }
    internal class InputView : MonoBehaviour
    {
        private InputListener _listener;
        [SerializeField] private InputAction[] _actions;
        private InputConfig.ActionKey _currentKey;
        private void Start()
        {
            _listener = GetComponent<InputListener>();
        }
        private void OnKeyPressed(KeyCode key)
        {

        }
    }
}
