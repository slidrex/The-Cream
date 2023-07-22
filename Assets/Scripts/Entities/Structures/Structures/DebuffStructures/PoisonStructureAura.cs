using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using Assets.Scripts.Entities.Stats.Structure.Aura;
using Assets.Scripts.Entities.Stats.Structure.Util;
using Assets.Scripts.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Structures.Structures.DebuffStructures
{
    internal class PoisonStructureAura : AuraStructure<MobTag>
    {
        [SerializeField] private GameObject _particles;
        [SerializeField] private float _damageTickInterval;
        [SerializeField] private int _damagePerTick;
        [SerializeField] private float _duration;
        [SerializeField] private float _percentSlow;
        protected override void OnActivate(Entity[] entitiesInRadius)
        {
            foreach (var entity in entitiesInRadius)
            {
                if(entity.ThisType is EntityType<MobTag>)
                    entity.Stats.ModifierHolder.AddModifier(new Poison(entity, _percentSlow, _damagePerTick, _duration, _damageTickInterval));
            }
            ParticlesUtil.SpawnParticles(_particles, transform);
        }
        protected override void OnActivateEntityTypeInsideAuraAndReady(List<Entity> entitiesOfActivateType)
        {
            TryActivate();
        }
    }
}
