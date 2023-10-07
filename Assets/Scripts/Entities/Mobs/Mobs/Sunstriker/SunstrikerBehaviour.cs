using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.StatAttributes;
using UnityEngine;

namespace Assets.Scripts.Entities.Mobs.Mobs.Sunstriker
{
    internal class SunstrikerBehaviour : EntityBrain<Sunstriker>
    {
        [SerializeField] private Sunstrike sunstrike;
        [SerializeField] private float attackRadius;
        private Animator animator;
        private const float _minTimeToAttack = 1;
        private const float _maxTimeToAttack = 3;
        

        private float attackSpeedModifier = 1;
        private float timeToNextAttack = 0;
        private float currentTime = 0;

        private void Start()
        {
            animator = GetComponent<Animator>();
            timeToNextAttack = Random.Range(_minTimeToAttack, _maxTimeToAttack);
        }
        protected override void RuntimeUpdate()
        {
            if (currentTime < timeToNextAttack)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                animator.SetTrigger("Attack");
                currentTime = 0;
                attackSpeedModifier = Entity.CurrentHealth / Entity.Stats.GetValue<MaxHealthStat>();
                attackSpeedModifier = Mathf.Clamp(attackSpeedModifier, 0.5f, 1);
                timeToNextAttack = Random.Range(_minTimeToAttack, _maxTimeToAttack) * attackSpeedModifier;
            }
        }

        public void InstantiateSunstrike()
        {
            var entities = NavigationUtil.GetAllEntitiesOfType(new EntityType<PlayerTag>().Any(), transform, attackRadius);

            if(entities.Count > 0)
            {
                byte rand = (byte)Random.Range(0, entities.Count);
                var strike = Instantiate(sunstrike, entities[rand].transform.position, Quaternion.identity);
                strike.Init(Entity);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}
