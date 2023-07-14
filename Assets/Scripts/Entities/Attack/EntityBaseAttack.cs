using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Movement;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Navigation.Navigator;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Attack
{
    [RequireComponent(typeof(Entity))]
    internal class EntityBaseAttack : MonoBehaviour, IMovementSafeDistanceProvider
    {
        [SerializeField] private Navigator _navigator;
        [SerializeField] private EntityMovement _movement;
        [SerializeField] private float _attackDistance;
        private Entity _entity;
        private ICanDamage _attackComponent;
        private float _timeSinceAttack;
        private float _timeToAttack;

        public float SafeDistance { get; private set; }

        private void Awake()
        {
            _entity = GetComponent<Entity>();
            _attackComponent = _entity as ICanDamage;
            if (_attackComponent == null) throw new NullReferenceException("Entity " + name + " doesn't contain ICanDamage interface");
            SafeDistance = _attackDistance;
            ResetAttackTimer();
        }
        private void Update()
        {
            if (LevelCompositeRoot.Instance.Runner.IsLevelRunning)
            {
                LevelUpdate();
            }
        }
        private void LevelUpdate()
        {
            if (_timeSinceAttack < _timeToAttack)
            {
                _timeSinceAttack += Time.deltaTime;
            }
            else
            {
                var target = _navigator.GetTarget();

                if (target != null && target is IDamageable && Vector2.SqrMagnitude(target.transform.position - transform.position) <= _attackDistance * _attackDistance) Attack(target);
            }
        }
        private void Attack(Entity target)
        {
            ResetAttackTimer();
            var damageable = target as IDamageable;
            damageable.Damage(_attackComponent.AttackDamage);
            OnAttack(target);
        }
        protected virtual void OnAttack(Entity target) { }
        private void ResetAttackTimer()
        {
            _timeToAttack = 1 / _attackComponent.AttackSpeed;
            _timeSinceAttack = 0;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}
