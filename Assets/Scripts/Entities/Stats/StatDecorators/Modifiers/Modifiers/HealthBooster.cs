using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers
{
    internal class HealthBooster : EntityStatModifier, IDurationable
    {
        public HealthBooster(EntityStats statProvider) : base(statProvider)
        {
        }

        public float Duration { get; set; } = 20.0f;

        public override bool ModifyStats()
        {
            IDamageable healthAttrib = StatsProvider as IDamageable;
            if (healthAttrib == null) return false;
            healthAttrib.MaxHealth = (int)(healthAttrib.MaxHealth *1.5f);
            return true;
        }

        public void UnmodifyStats()
        {
            IDamageable healthAttrib = StatsProvider as IDamageable;

            healthAttrib.MaxHealth = (int)(healthAttrib.MaxHealth / 1.5f);
        }
    }
}
