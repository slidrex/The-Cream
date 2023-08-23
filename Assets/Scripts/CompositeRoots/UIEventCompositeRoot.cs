using Assets.Scripts.UI.PopupEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CompositeRoots
{
    internal class UIEventCompositeRoot : MonoBehaviour
    {
        public static UIEventCompositeRoot Instance;
        public AlertText LevelTitle;
        public ModelErrorState ErrorModel;
        private void Awake()
        {
            Instance = this;
        }
    }
}
