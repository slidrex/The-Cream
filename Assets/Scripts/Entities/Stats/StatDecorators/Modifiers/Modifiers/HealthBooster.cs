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

        public override bool ModifyStats()
        {
            IDamageable healthAttrib = StatsProvider as IDamageable;
            if (healthAttrib == null) return false;
            healthAttrib.MaxHealth = (int)(healthAttrib.MaxHealth *1.5f);
            return true;
        }

        public override void UnmodifyStats()
        {
            IDamageable healthAttrib = StatsProvider as IDamageable;

            healthAttrib.MaxHealth = (int)(healthAttrib.MaxHealth / 1.5f);
        }
    }
}
