using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers
{
    internal class InstantDamage : EntityStatModifier
    {
        private float _maxHealthDamagePercent;
        public InstantDamage(Entity statProvider, float maxHealthDamagePercent) : base(statProvider)
        {
            _maxHealthDamagePercent = maxHealthDamagePercent;
        }

        public override bool OnEffectStart()
        {
            if (StatsProvider is IDamageable damageable)
            {
                damageable.Damage((int)(damageable.MaxHealth * _maxHealthDamagePercent), null);
                return true;
            }
            return false;
        }
    }
}
