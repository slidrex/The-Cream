using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills
{
    internal abstract class PlayerActiveSkill : PlayerSkill
    {
        public int BaseManacost;
        public float BaseCooldown;
        public float TimeSinceActivation { get; private set; }
        public abstract bool TryActivate(SkillHolder holder, Player player);
        public override void Update()
        {
            if (TimeSinceActivation < BaseCooldown)
            {
                TimeSinceActivation += Time.deltaTime;
            }
        }
        protected void ResetTimer()
        {
            TimeSinceActivation = 0;
        }
    }
}
