using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Player.Characters;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
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
        [SerializeField] private ParticleSystem _explosionParticles;
        [SerializeField] private float _slowDuration;
        [SerializeField, Range(0, 1.0f)] private float _slowPercentage;
        [SerializeField] private string _explosionTrigger;
        [SerializeField] private float _radius;
        private List<Entity> foundEntities = new List<Entity>();
        private EntityTypeBase _targetEntities = new EntityType<PlayerTag>().Any();
        [SerializeField] private float _explosionInterval;
        private float _timeSinceExplosion;
        private Movement movement;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            movement = GetComponent<Movement>();
        }
        public void OnExplosion()
        {
            _explosionParticles.Play();
            if (foundEntities.Count > 0)
            {
                foreach (var e in foundEntities)
                {
                    e.Stats.ModifierHolder.AddModifier(new SpeedBooster(e, -_slowPercentage) { Duration = _slowDuration });
                    (e as IDamageable).Damage((int)Entity.Stats.GetValue<DamageStat>(), Entity);
                }
            }
            movement.EnableMovement(true);
        }
        protected override void RuntimeUpdate()
        {
            if (_timeSinceExplosion < _explosionInterval)
            {
                _timeSinceExplosion += Time.deltaTime;
            }
            else
            {
                foundEntities = NavigationUtil.GetAllEntitiesOfType(_targetEntities, transform, _radius);
                if(foundEntities.Count > 0)
                {
                    _timeSinceExplosion = 0;
                    movement.EnableMovement(false);
                    _animator.SetTrigger(_explosionTrigger);
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
