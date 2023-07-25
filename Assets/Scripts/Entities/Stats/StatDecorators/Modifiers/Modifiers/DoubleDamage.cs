using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers
{
    internal class DoubleDamage : EntityStatModifier, IDurationable
    {
        private AttributeMask _damageMask = new AttributeMask() { BaseMultiplier = 2 };

        public DoubleDamage(Entity statProvider, float duraton) : base(statProvider)
        {
            Duration = duraton;
        }

        public float Duration { get; set; }

        public void OnEffectEnd() => StatsProvider.Stats.Unmodify<DamageStat>(_damageMask);

        public override bool OnEffectStart() => StatsProvider.Stats.Modify<DamageStat>(_damageMask);
    }
}
