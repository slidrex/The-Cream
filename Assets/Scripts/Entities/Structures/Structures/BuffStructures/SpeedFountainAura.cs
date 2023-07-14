using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
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

namespace Assets.Scripts.Entities.Structures.Structures.BuffStructures
{
    internal class SpeedFountainAura : AuraStructure<PlayerTag>
    {
        [SerializeField] private GameObject _particles;
        private PullingUtil _pullUtil = new();
        [UnityEngine.SerializeField] private float _speedMultiplier;
        private void OnEnable() => _pullUtil.OnStart(OnRegister);
        private void OnDisable() => _pullUtil.OnEnd();
        private void OnRegister(Entity entity, bool register) => _pullUtil.TryRegister(entity, register, out var e);
        protected override void OnActivate(Entity[] entitiesInRadius)
        {
            foreach (var entity in entitiesInRadius)
            {
                entity.StatModifierHandler.AddModifier(new SpeedBooster(entity, _speedMultiplier));
            }
            ParticlesUtil.SpawnParticles(_particles, transform);
        }
        protected override void OnAuraBecomeReady()
        {
            _pullUtil.PullAll(transform);
        }
        protected override void OnActivateEntityTypeInsideAuraAndReady(List<Entity> entitiesOfActivateType)
        {
            TryActivate();
        }
    }
}
