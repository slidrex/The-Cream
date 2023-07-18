using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/New Active")]
    internal class PlayerActiveSkill : PlayerSkill
    {
        public int BaseManacost;
        public float BaseCooldown;
        private float _timeSinceActivation;
        public Boolean TryActivate(Player player)
        {
            if (_timeSinceActivation < BaseCooldown) return false;
            OnActivate(player);
            _timeSinceActivation = 0;
            return true;
        }
        public void Update()
        {
            if(_timeSinceActivation < BaseCooldown)
            {
                _timeSinceActivation += Time.deltaTime;
            }
        }
        protected virtual void OnActivate(Player player)
        {

        }
    }
}
