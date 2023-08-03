using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LevelEditor.Ability
{
    internal class AbilityAdapter : MonoBehaviour
    {
        public static AbilityAdapter Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
        public void StartAbility()
    }
}
