using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using Assets.Scripts.Entities.Stats.Structure.Aura;
using Assets.Scripts.Entities.Stats.Structure.Util;
using Assets.Scripts.Entities.Util.Events;
using Assets.Scripts.Entities.Util.Events.EventAlert;
using Assets.Scripts.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.ParticleSystem;

namespace Assets.Scripts.Entities.Structures.Structures.BuffStructures
{
    internal class HealingFountainAura : AuraStructure<PlayerTag>
    {
		[field: SerializeField] public Sprite AlertSprite { get; set; }

		private AlertUtil _alertUtil;

        [SerializeField] private GameObject _particles;
        [UnityEngine.SerializeField, Range(0, 1f)] private float _damagePercentBeforePulling;
        [UnityEngine.SerializeField, Range(0, 1f)] private float _healingPercent;


		private void Start()
		{
            _alertUtil = new(AlertSprite, transform, 1, 0.5f);
		}
		private void OnTargetChangeHealth(PullingUtil.PullableEntity entity, int newHealth)
        {
            if(IsReady && newHealth <= entity.Entity.Stats.GetAttribute<MaxHealthStat>().GetValue() * _damagePercentBeforePulling)
            {
                entity.Pullable.Pull(transform);
            }
        }
        protected override void OnActivate(Entity[] entitiesInRadius)
        {
            foreach(var entity in entitiesInRadius)
            {
                if(entity.ThisType is EntityType<PlayerTag>)
                    entity.Stats.ModifierHolder.AddModifier(new InstantHeal(entity, _healingPercent));
            }
            ParticlesUtil.SpawnParticles(_particles, transform);
			_alertUtil.EnableMark(false);
		}
        protected override void OnAuraBecomeReady()
        {
			_alertUtil.EnableMark(true);
		}
        protected override void OnActivateEntityTypeInsideAuraAndReady(List<Entity> entitiesOfActivateType)
        {
            TryActivate();
        }
    }
}
