using Assets.Scripts.Entities.AI.ContextSteering;
using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.AI.Surrounding;
using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Attack
{
    [RequireComponent(typeof (Movement))]
    [RequireComponent(typeof (SteeringMovement))]
    [RequireComponent(typeof(Facing))]
    internal class MobBaseAttack : EntityBrain<Entity>
    {
        [SerializeField] private bool _showGizmos;
        [SerializeField] private float _minCircleDistance;
        [SerializeField] private float _maxCircleDistance;
        private Facing _facing;
        private DamageStat _attackComponent;
        private AttackSpeedStat _attackSpeed;
        protected float _timeSinceAttack;
        protected float _timeToAttack;
        protected EnvironmentData _data;
        protected Movement Movement;
        private SteeringMovement _steeringBehaviour;
        private CircleMovement _circleMovement;
        private const float BIND_TIME = 1.5f;
        private float _timeSinceBind;
        private bool _steeringBinded;
        protected virtual void Start()
        {
            _facing = GetComponent<Facing>();
			_circleMovement = GetComponent<CircleMovement>();
			Movement = GetComponent<Movement>();
            _data = GetComponent<EnvironmentData>();
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
                _data.SetTarget(NavigationUtil.GetClosestEntityOfType(Entity.TargetType, Entity));
            if (_data.CurrentTarget == null) return;
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
            _facing.StalkTarget(curTarget);
            Vector2 steeringDir = _steeringBehaviour.GetDirectionToMove();
            float sqrDist = Vector2.SqrMagnitude(curTarget.position - transform.position);
            Vector2 circleDir = _circleMovement.GetMoveDirection(curTarget, _minCircleDistance, _maxCircleDistance);

            if(_timeSinceBind <= 0)
            {
			    if (sqrDist > _minCircleDistance * _minCircleDistance && sqrDist < _maxCircleDistance * _maxCircleDistance)
                {
                    _steeringBinded = false;
                }
                else
                {
                    _steeringBinded = true;
                }
                _timeSinceBind = BIND_TIME;
            }
            Vector2 targetDir = circleDir;// _steeringBinded ? steeringDir;// : circleDir;


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
		private void OnDrawGizmos()
		{
            if (_showGizmos)
            {
			    Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, _minCircleDistance);
			    Gizmos.DrawWireSphere(transform.position, _maxCircleDistance);
            }
		}
	}
}
