using Assets.Scripts.Entities.Stats.Interfaces.Stats;
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
    internal class InstantHealth : EntityStatModifier
    {
        private float _maxHealthHealPercent;
        public InstantHealth(EntityStats statProvider, float healPercentFromMax) : base(statProvider)
        {
            _maxHealthHealPercent = healPercentFromMax;
        }

        public override bool ModifyStats()
        {
            if (StatsProvider is IDamageable damageable)
            {
                damageable.Heal((int)(damageable.MaxHealth * _maxHealthHealPercent));
                return true;
            }
            return false;
        }
    }
}
