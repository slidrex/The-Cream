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
    internal class ModelErrorState : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent _errorLocalizer;
        [SerializeField] private Transform _errorContainer;
        [SerializeField] private float _errorDisplayTime;
        public void AddError(string messageKey)
        {
            var error = Instantiate(_errorLocalizer, _errorContainer);
            error.SetEntry(messageKey);
            error.RefreshString();
            Destroy(error.gameObject, _errorDisplayTime);
        }
    }
}
