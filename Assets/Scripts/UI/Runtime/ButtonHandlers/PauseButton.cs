using System;
using Assets.Scripts.UI.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime.ButtonHandlers
{
    public class PauseButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnPauseEnable);
        }

        private void OnPauseEnable()
        {
            ScreenController.Instance.EnableScreen(ScreenController.Screen.PAUSE);
        }
    }
}