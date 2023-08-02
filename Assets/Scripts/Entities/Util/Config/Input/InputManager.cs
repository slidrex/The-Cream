using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Util.Config.Input
{
    internal class InputManager : MonoBehaviour
    {
        private static Dictionary<KeyCode, Action> _keyBindings = new();
        public static void Bind(KeyCode keycode, Action action)
        {
            if(keycode != KeyCode.None && _keyBindings.TryAdd(keycode, action) == false)
            {
                _keyBindings[keycode] = action;
            }
        }
        private void Update()
        {
            if (UnityEngine.Input.anyKey)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (UnityEngine.Input.GetKeyDown(keyCode) && _keyBindings.TryGetValue(keyCode, out Action action))
                    {
                        action.Invoke();
                    }
                }
            }
        }
    }
}
