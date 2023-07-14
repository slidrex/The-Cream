using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers
{
    internal class SpeedBooster : EntityStatModifier, IDurationable
    {
        private float _speedMultiplier;
        public SpeedBooster(Entity statProvider, float speedMultiplier) : base(statProvider)
        {
            _speedMultiplier = speedMultiplier;
        }
        public float Duration { get; set; }

        public void OnEffectEnd()
        {
            (StatsProvider as IMoveable).CurrentSpeed /= _speedMultiplier;
        }

        public override bool OnEffectStart()
        {
            if (StatsProvider is IMoveable move)
            {
                move.CurrentSpeed *= _speedMultiplier;
                return true;
            }
            return false;
        }
    }
}
