using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Util.Config.Menu
{
    internal class InputListener : MonoBehaviour
    {
        private Action<KeyCode> _listeningAction;
        public void ListenForKey(Action<KeyCode> key)
        {
            _listeningAction = key;
        }
        private void Update() 
        {
            if(_listeningAction != null && UnityEngine.Input.anyKey)
            {
                GetListenedKey();
            }
        }
        private void GetListenedKey()
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (UnityEngine.Input.GetKeyDown(keyCode))
                {
                    _listeningAction.Invoke(keyCode);
                    _listeningAction = null;
                    return;
                }
            }
        }
    }
}
