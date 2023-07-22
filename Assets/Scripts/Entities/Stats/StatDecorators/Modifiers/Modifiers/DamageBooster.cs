using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers
{
    internal class DamageBooster : EntityStatModifier, IDurationable
    {
        private AttributeMask _damageMask;
        public float Duration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DamageBooster(Entity statProvider, float percentage) : base(statProvider) => _damageMask = new AttributeMask() { MaskMultiplier = percentage };

        public void OnEffectEnd() => StatsProvider.Stats.Unmodify<DamageStat>(_damageMask);

        public override bool OnEffectStart() => StatsProvider.Stats.Modify<DamageStat>(_damageMask);
    }
}
