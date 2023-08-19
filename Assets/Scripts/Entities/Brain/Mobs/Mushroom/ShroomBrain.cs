﻿using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Player.Characters;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Brain.Mobs.Mushroom
{
    internal class ShroomBrain : EntityBrain<Player.Characters.Mushroom>
    {
        private Animator _animator;
        [SerializeField] private int _damage;
        [SerializeField] private GameObject _explosionParticles;
        [SerializeField] private float _slowDuration;
        [SerializeField, Range(0, 1.0f)] private float _slowPercentage;
        [SerializeField] private string _explosionTrigger;
        [SerializeField] private float _radius;
        private EntityTypeBase _targetEntities = new EntityType<PlayerTag>().Any();
        [SerializeField] private float _explosionInterval;
        private float _timeSinceExplosion;
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        private void OnExplosion(List<Entity> entities)
        {
            var obj = Instantiate(_explosionParticles, transform.position, Quaternion.identity);
            Destroy(obj, 5f);
            _animator.SetTrigger(_explosionTrigger);
            foreach(var e in entities)
            {
                e.Stats.ModifierHolder.AddModifier(new SpeedBooster(e, -_slowPercentage) { Duration = _slowDuration });
                (e as IDamageable).Damage(_damage, Entity);
            }
        }
        protected override void RuntimeUpdate()
        {
            if (_timeSinceExplosion < _explosionInterval)
            {
                _timeSinceExplosion += Time.deltaTime;
            }
            else
            {
                var entities = NavigationUtil.GetAllEntitiesOfType(_targetEntities, transform, _radius);
                if(entities.Count > 0)
                {
                    _timeSinceExplosion = 0;
                    OnExplosion(entities);
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
