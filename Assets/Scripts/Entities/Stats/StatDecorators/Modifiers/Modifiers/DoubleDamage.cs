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
        private Action _onAbilityEnd;

        public DoubleDamage(Entity statProvider, float duraton, Action onAbilityEnd) : base(statProvider)
        {
            _onAbilityEnd = onAbilityEnd;
            Duration = duraton;
        }

        public float Duration { get; set; }

        public void OnEffectEnd()
        {
            StatsProvider.Stats.Unmodify<DamageStat>(_damageMask);
            _onAbilityEnd.Invoke();
        }

        public override bool OnEffectStart() => StatsProvider.Stats.Modify<DamageStat>(_damageMask);
    }
}
