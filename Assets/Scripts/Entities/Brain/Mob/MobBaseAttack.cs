using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.AI.ContextSteering;
using Assets.Scripts.Entities.AI.Surrounding;
using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Movement;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Navigation.Navigator;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Attack
{
    [RequireComponent(typeof (EntityMovement))]
    [RequireComponent(typeof (SteeringMovement))]
    internal class MobBaseAttack : EntityBrain
    {
        [SerializeField] private float _minSafe;
        [SerializeField] private float _maxSafe;
        private DamageStat _attackComponent;
        private AttackSpeedStat _attackSpeed;
        protected float _timeSinceAttack;
        protected float _timeToAttack;
        protected AIData _data;
        protected EntityMovement Movement;
        private SteeringMovement _steeringBehaviour;
        private CircleMovement _circleMovement;
        private const float BIND_TIME = 1.5f;
        private float _timeSinceBind;
        private bool _steeringBinded;
        protected virtual void Start()
        {
			_circleMovement = GetComponent<CircleMovement>();
			Movement = GetComponent<EntityMovement>();
            _data = GetComponent<AIData>();
            _steeringBehaviour = GetComponent<SteeringMovement>();
            _steeringBehaviour.ConsiderSafeDistance = true;
            _attackComponent = Entity.Stats.GetAttribute<DamageStat>();
            _attackSpeed = Entity.Stats.GetAttribute<AttackSpeedStat>();

            if (_attackComponent == null) throw new NullReferenceException("Entity " + name + " doesn't contain ICanDamage interface");

            ResetAttackTimer();
        }
        protected override void RuntimeUpdate()
        {
            if (_data.CurrentTarget == null)
                _data.SetTarget(NavigationUtil.GetClosestEntityOfType(Entity.TargetType, transform));
            
            if (_timeSinceBind > 0) _timeSinceBind -= Time.deltaTime;

            if (_timeSinceAttack < _timeToAttack)
            {
                _timeSinceAttack += Time.deltaTime;
            }
            else
            {
                var target = _data.CurrentTargetEntity;
                if (target != null && target is IDamageable && _data.IsReachedTarget) Attack(target);
            }
            var curTarget = _data.CurrentTarget;
            StalkTarget(curTarget);
            Vector2 steeringDir = _steeringBehaviour.GetDirectionToMove();
            float sqrDist = Vector2.SqrMagnitude(curTarget.position - transform.position);
            Vector2 circleDir = _circleMovement.GetMoveDirection(curTarget, _minSafe, _maxSafe);

            if(_timeSinceBind <= 0)
            {
			    if (sqrDist > _minSafe * _minSafe && sqrDist < _maxSafe * _maxSafe)
                {
                    _steeringBinded = false;
                }
                else
                {
                    _steeringBinded = true;
                }
                _timeSinceBind = BIND_TIME;
            }
            Vector2 targetDir = _steeringBinded ? steeringDir : circleDir;
            //Vector2 targetDir = circleDir;

			Movement.SetMoveDirection(targetDir);
        }
        protected void Attack(Entity target)
        {
            ResetAttackTimer();
            var damageable = target as IDamageable;
            damageable.Damage((int)_attackComponent.GetValue(), Entity);
            OnAttack(target);
        }
        protected virtual void OnAttack(Entity target) { }
        private void ResetAttackTimer()
        {
            _timeToAttack = 1 / _attackSpeed.GetValue();
            _timeSinceAttack = 0;
        }
    }
}
