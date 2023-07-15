using Assets.Scripts.Entities.Stats.Interfaces.Stats;
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
        [UnityEngine.SerializeField] private float _damageMultiplier;
        public DamageBooster(Entity statProvider, float multiplier) : base(statProvider)
        {
            _damageMultiplier = multiplier;
        }

        public float Duration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void OnEffectEnd()
        {
            var d = StatsProvider as ICanDamage;
            d.AttackDamage = (int)(d.AttackDamage/ _damageMultiplier);
        }

        public override bool OnEffectStart()
        {
            if (StatsProvider is ICanDamage d)
            {
                d.AttackDamage = (int)(d.AttackDamage * _damageMultiplier);
                return true;
            }
            return false;
        }
    }
}
