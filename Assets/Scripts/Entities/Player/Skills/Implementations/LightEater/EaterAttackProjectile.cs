using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.Environment;
using System.Collections;
using UnityEngine;

namespace Entities.Player.Skills.Implementations.LightEater
{
    internal class EaterAttackProjectile : AttackProjectile
    {
        [SerializeField] private GameObject destroyParticles;
        public override EntityTypeBase TriggerEntityType => new EntityType<MobTag>().Any();
        protected override void OnTargetHit(Entity hit)
        {
            base.OnTargetHit(hit);
            StartCoroutine(OnDestroyProjectle());
        }

        private IEnumerator OnDestroyProjectle()
        {
            GetCollider().enabled = false;
            float minSize = 0.3f;
            int multiplyValue = 8;

            while (transform.localScale.x > minSize)
            {
                transform.localScale =
                    new Vector2(transform.localScale.x - Time.deltaTime * multiplyValue, transform.localScale.y - Time.deltaTime * multiplyValue);
                yield return new WaitForEndOfFrame();
            }
            
            ParticlesUtil.SpawnParticles(destroyParticles, transform);
            Destroy(gameObject);
        }
    }
}