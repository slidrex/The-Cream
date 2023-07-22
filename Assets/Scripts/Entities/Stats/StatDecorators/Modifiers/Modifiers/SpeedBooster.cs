using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
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
        private AttributeMask _speedMask;
        public SpeedBooster(Entity statProvider, float speedPercentage) : base(statProvider)
        {
            _speedMask = new() { MaskMultiplier = -speedPercentage };
        }
        public float Duration { get; set; }

        public void OnEffectEnd()
        {
            StatsProvider.Stats.Unmodify<SpeedStat>(_speedMask);
        }

        public override bool OnEffectStart()
        {
            return StatsProvider.Stats.Modify<SpeedStat>(_speedMask);
        }
    }
}
