using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.SpecialAbilities
{
    internal class Shield : MonoBehaviour
    {
        [SerializeField] private GameObject _particles;
        [SerializeField] private int damage;
        [SerializeField] private float _stunTime;
        [SerializeField] private float lifetime;
        [SerializeField] private float _radius;
        private EntityTypeBase _targetEntities = new EntityType<MobTag>().Any();
        private Entity _issuer;
        public void Configure(Entity entity)
        {
            _issuer = entity;
            transform.position = entity.transform.position + Vector3.right * entity.GetComponent<Facing>().CurrentSight;
            Destroy(gameObject, lifetime);
            ParticlesUtil.SpawnParticles(_particles, transform);
            Explode();
        }
        private void Explode()
        {
            var entities = NavigationUtil.GetAllEntitiesOfType(_targetEntities, transform, _radius);
            foreach (var entity in entities)
            {
                (entity as IDamageable).Damage(damage, _issuer);
                if(entity.TryGetComponent<Movement>(out var move))
                {
                    move.EnableMovement(false, _stunTime);
                }

            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

    }
}
