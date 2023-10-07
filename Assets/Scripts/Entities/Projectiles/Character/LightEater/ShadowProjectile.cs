using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Environment;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Entities.Projectiles.Character.LightEater
{
    internal class ShadowProjectile : AttackProjectile
    {
        [SerializeField] private GameObject destroyParticles;
        private float _directionTime;
        private float _timeSinceShoot;
        public override EntityTypeBase TriggerEntityType => new EntityType<MobTag>().Any();
        public void InitTime(float directionTime)
        {
            _directionTime = directionTime;
            StartCoroutine(SmoothFade());
        }
        private void Update()
        {
            OnBeforeReturn();
        }
        private void OnBeforeReturn()
        {
            if(_timeSinceShoot < _directionTime)
            {
                _timeSinceShoot += Time.deltaTime;
                if(_timeSinceShoot >= _directionTime)
                {
                    SetMoveDirection(-MoveVector);
                }
            }
        }

        private IEnumerator SmoothFade()
        {
            float timeToFade = GetLifeTime() - 0.3f;
            float minSize = 0.1f;
            int reductionSpeed = 4;

            yield return new WaitForSeconds(timeToFade);

            while(transform.localScale.x > minSize)
            {
                transform.localScale = 
                    new Vector2(transform.localScale.x - Time.deltaTime * reductionSpeed, transform.localScale.y - Time.deltaTime * reductionSpeed);
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnDestroy()
        {
            ParticlesUtil.SpawnParticles(destroyParticles, transform);
            Destroy(gameObject);
        }
    }
}
