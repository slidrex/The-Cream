using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers
{
    /// <summary>
    /// Slows and periodically damages.
    /// </summary>
    internal class Poison : EntityStatModifier, ITickable
    {
        private AttributeMask _speedMask;

        private int _tickDamage;
        private SpriteRenderer _renderer;
        public Poison(Entity statProvider, float percentSlow, int tickDamage, float duration, float damageInterval) : base(statProvider)
        {
            _speedMask = new AttributeMask() { MaskMultiplier = -percentSlow };
            _tickDamage = tickDamage;
            _renderer = statProvider.GetComponent<SpriteRenderer>();
            CallInterval = damageInterval;
            Duration = duration;
        }

        public float CallInterval { get; set; } = 1.0f;
        public float Duration { get; set; } = 8.0f;

        public void OnEffectEnd()
        {
            StatsProvider.Stats.Unmodify<SpeedStat>(_speedMask);
        }

        public override bool OnEffectStart()
        {
            if (StatsProvider.Stats.Modify<SpeedStat>(_speedMask))
            {
                _renderer.color = Color.green;
                return true;
            }
            return false;
        }

        public void OnTick()
        {
            if (StatsProvider is IDamageable damageable)
            {
                damageable.Damage(_tickDamage, null);
            }
        }
    }
}
