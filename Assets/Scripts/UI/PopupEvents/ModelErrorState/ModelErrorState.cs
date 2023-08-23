using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.PopupEvents
{
    internal class ModelErrorState : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _errorText;
        [SerializeField] private Transform _errorContainer;
        [SerializeField] private float _errorDisplayTime;
        public void AddError(string message)
        {
            var error = Instantiate(_errorText, _errorContainer);
            error.text = message;
            Destroy(error, _errorDisplayTime);
        }
    }
}
