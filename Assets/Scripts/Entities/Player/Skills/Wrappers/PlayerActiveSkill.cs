using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills
{
    internal class PlayerActiveSkill : PlayerSkill
    {
        public int BaseManacost;
        public float BaseCooldown;
        private float _timeSinceActivation;
        public bool TryActivate(Player player)
        {
            var playerSpace = Editor.Editor.Instance.PlayerSpace;

            if (_timeSinceActivation < BaseCooldown || !playerSpace.TrySpendMana(BaseManacost)) return false;
            OnActivate(player);

            _timeSinceActivation = 0;
            return true;
        }
        public override void Update()
        {
            if (_timeSinceActivation < BaseCooldown)
            {
                _timeSinceActivation += Time.deltaTime;
            }
        }
        protected virtual void OnActivate(Player player)
        {

        }
    }
}
