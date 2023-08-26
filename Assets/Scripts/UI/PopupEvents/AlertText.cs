using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Assets.Scripts.UI.PopupEvents
{
    internal class AlertText : MonoBehaviour
    {
        [SerializeField] private bool _disableText;
        [SerializeField] private AnimationCurve _alphaCurve;
        [SerializeField] private float _size;
        [SerializeField] private float _duration;
        [SerializeField] private LocalizeStringEvent _localizator;
        private TextMeshProUGUI _alertText;
        private float _timeSinceEnabling;
        private float _targetTime;
        private bool _isEnabled;
        private void Awake()
        {
			_localizator = GetComponent<LocalizeStringEvent>();
			_alertText = GetComponent<TextMeshProUGUI>();
        }
		public void EnableText()
        {
            if (_disableText) return;
            gameObject.SetActive(true);
            _isEnabled = true;
            _localizator.RefreshString();
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
