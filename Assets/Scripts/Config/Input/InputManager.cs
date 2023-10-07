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
        private static Dictionary<KeyCode, string> KEY_PSUEDONAMES = new Dictionary<KeyCode, string>()
        {
            [KeyCode.None] = "",
            [KeyCode.Mouse0] = "M0",
            [KeyCode.Mouse1] = "M1",
            [KeyCode.Mouse3] = "M3",
            [KeyCode.Mouse4] = "M4",
            [KeyCode.Mouse5] = "M5",
            [KeyCode.Mouse6] = "M6",
            [KeyCode.Underscore] = "_",
            [KeyCode.Return] = "Enter",
            [KeyCode.LeftShift] = "LShift",
            [KeyCode.RightShift] = "RShift",
            [KeyCode.UpArrow] = "↑",
            [KeyCode.DownArrow] = "↓",
            [KeyCode.LeftArrow] = "←",
            [KeyCode.RightArrow] = "→",
            [KeyCode.Alpha0] = "0",
            [KeyCode.Alpha1] = "1",
            [KeyCode.Alpha2] = "2",
            [KeyCode.Alpha3] = "3",
            [KeyCode.Alpha4] = "4",
            [KeyCode.Alpha5] = "5",
            [KeyCode.Alpha6] = "6",
            [KeyCode.Alpha7] = "7",
            [KeyCode.Alpha8] = "8",
            [KeyCode.Alpha9] = "9"
        };
        public static string GetKeyName(KeyCode keyCode)
        {
            if (KEY_PSUEDONAMES.TryGetValue(keyCode, out string name)) return name;
            else return keyCode.ToString();
        }
        private static Dictionary<KeyCode, Action> _keyBindings = new();
        public static void Bind(KeyCode keycode, Action action)
        {
            if(keycode != KeyCode.None && _keyBindings.TryAdd(keycode, action) == false)
            {
                _keyBindings[keycode] = action;
            }
        }
        public static bool IsActionKeyPressed(bool down, out Vector2 clickPosition) 
        {
            Vector2 rawPos = Vector2.zero;
            bool isSuccess = false;
            if(UnityEngine.Input.GetKeyDown(KeyCode.Mouse1) || UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
				rawPos = UnityEngine.Input.mousePosition;
                isSuccess = true;
			}
            else if(down == false && (UnityEngine.Input.GetKey(KeyCode.Mouse1) || UnityEngine.Input.GetKey(KeyCode.Mouse0)))
            {
                rawPos = UnityEngine.Input.mousePosition;
                isSuccess = true;
            }
            else if (UnityEngine.Input.touchCount > 1)
            {
                rawPos = UnityEngine.Input.GetTouch(0).position;
                isSuccess = true;
			}

            clickPosition = Vector2.zero;
            if(rawPos != Vector2.zero)  clickPosition = Editor.Editor.Camera.ScreenToWorldPoint(rawPos);
            return isSuccess;
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
