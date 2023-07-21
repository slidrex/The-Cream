using Assets.Scripts.Entities.Stats.Interfaces.Stats;
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
        private int _tickDamage;
        private SpriteRenderer _renderer;
        public Poison(Entity statProvider, int tickDamage, float duration, float damageInterval) : base(statProvider)
        {
            _tickDamage = tickDamage;
            _renderer = statProvider.GetComponent<SpriteRenderer>();
            CallInterval = damageInterval;
            Duration = duration;
        }

        public float CallInterval { get; set; } = 1.0f;
        public float Duration { get; set; } = 8.0f;

        public void OnEffectEnd()
        {
            if (StatsProvider is IMoveable moveable)
            {
                _renderer.color = StatsProvider.DefaultColor;
                moveable.CurrentSpeed *= 5;
            }
        }

        public override bool OnEffectStart()
        {
            if(StatsProvider is IMoveable moveable)
            {
                _renderer.color = Color.green;
                moveable.CurrentSpeed /= 5;
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
