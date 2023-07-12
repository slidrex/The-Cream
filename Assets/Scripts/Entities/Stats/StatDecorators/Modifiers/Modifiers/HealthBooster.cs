using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers
{
    internal class HealthBooster : EntityStatModifier
    {
        public HealthBooster(EntityStats statProvider) : base(statProvider)
        {
        }

        internal override float Duration => 20.0f;

        public override EntityStats GetStats()
        {
            if(StatsProvider is IDamageable componenet)
            {
                componenet.MaxHealth = (int)(componenet.MaxHealth * 1.5f);
            }
            return StatsProvider;
        }
    }
}
