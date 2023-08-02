using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using Assets.Scripts.Entities.Stats.Structure.Aura;
using Assets.Scripts.Entities.Stats.Structure.Util;
using Assets.Scripts.Entities.Util.Events.EventAlert;
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
		private AlertUtil _alertUtil;
		[SerializeField] private GameObject _particles;
        [UnityEngine.SerializeField] private float _speedMultiplier;
		private void Start()
		{
            _alertUtil = new(null, transform, 1, 0.5f);
		}
		protected override void OnActivate(Entity[] entitiesInRadius)
        {
            foreach (var entity in entitiesInRadius)
            {
                if (entity.ThisType is EntityType<PlayerTag>)
                    entity.Stats.ModifierHolder.AddModifier(new SpeedBooster(entity, _speedMultiplier));
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
