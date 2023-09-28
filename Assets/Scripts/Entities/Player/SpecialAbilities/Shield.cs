using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using Assets.Scripts.Environment;
using System.Collections;
using System.Reflection.Emit;
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
            transform.position = transform.position + (Vector3.right * 0.7f) * entity.GetComponent<Facing>().CurrentSight;
            StartCoroutine(ShieldAttackAnimation(entity.GetComponent<Facing>().CurrentSight));
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
            ParticlesUtil.SpawnParticles(_particles, transform);
        }
        private IEnumerator ShieldAttackAnimation(int sight)
        {
            int speed = 10;
            float currentTime = 0;
            float maxTime = 0.1f;
            while (currentTime < maxTime)
            {
                currentTime += Time.deltaTime;
                transform.localScale =
                    new Vector2(transform.localScale.x + Time.deltaTime * speed, transform.localScale.y + Time.deltaTime * speed);
                transform.position = transform.position + (Vector3.right * speed * Time.deltaTime) * sight;
                yield return new WaitForEndOfFrame();
            }

            Explode();
            currentTime = 0;

            while (currentTime < maxTime)
            {
                currentTime += Time.deltaTime;
                transform.localScale =
                    new Vector2(transform.localScale.x - Time.deltaTime * speed * 1.5f, transform.localScale.y - Time.deltaTime * speed * 1.5f);
                transform.position = Vector2.MoveTowards(transform.position, _issuer.transform.position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject, 0.05f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

    }
}
