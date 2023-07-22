using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers
{
    /// <summary>
    /// Heals percent from max health
    /// </summary>
    internal class InstantHeal : EntityStatModifier
    {
        private float _maxHealthHealPercent;
        public InstantHeal(Entity statProvider, float healPercentFromMax) : base(statProvider)
        {
            _maxHealthHealPercent = healPercentFromMax;
        }

        public override bool OnEffectStart()
        {
            if (StatsProvider is IDamageable damageable)
            {
                damageable.Heal((int)(StatsProvider.Stats.GetValue<MaxHealthStat>() * _maxHealthHealPercent));
                return true;
            }
            return false;
        }
    }
}
