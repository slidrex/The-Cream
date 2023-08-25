using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.PopupEvents
{
    internal class AlertText : MonoBehaviour
    {
        [SerializeField] private bool _disableText;
        [SerializeField] private AnimationCurve _alphaCurve;
        [SerializeField] private float _size;
        [SerializeField] private float _duration;
        private TextMeshProUGUI _alertText;
        private float _timeSinceEnabling;
        private float _targetTime;
        private bool _isEnabled;
        private void Awake()
        {
            _alertText = GetComponent<TextMeshProUGUI>();
        }
        public void EnableText(string text)
        {
            if (_disableText) return;
            gameObject.SetActive(true);
            _isEnabled = true;
            _alertText.text = text;

            _targetTime = _duration;
            _timeSinceEnabling = 0.0f;
        }
        private void DisableText()
        {
            gameObject.SetActive(false);
            _isEnabled = false;
        }
        private void Update()
        {
            if(_isEnabled)
                UpdateAlertText();
        }
        private void UpdateAlertText()
        {
            if(_timeSinceEnabling >= _targetTime)
            {
                DisableText();
            }
            else
            {
                _timeSinceEnabling += Time.deltaTime;
                _alertText.alpha = _alphaCurve.Evaluate(_timeSinceEnabling/_targetTime);

            }
        }
    }
}
