using System;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.AI.ContextSteering;
using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Player.Moving;
using Assets.Scripts.Entities.Stats.Interfaces.Attack;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Player.Components.Attacking
{
    internal class PlayerAttack : EntityBrain<Assets.Scripts.Entities.Player.Player>
    {
        [SerializeField] private bool _manualAttack = true;
		[SerializeField] private ParticleSystem attackParticles;
		[SerializeField] private string[] ATTACK_TRIGGER = { "Attack", "AlternativeAttack" };
		private float AttackSpeed => Entity.Stats.GetValue<AttackSpeedStat>();
		protected int Damage => Entity.Stats.GetValueInt<DamageStat>();
		private Facing _facing;
		protected EnvironmentData Data { get; private set; }
		private float _timeToAttack;
		private float _timeSinceAttack;
		private Animator _animator;
		private bool _inAttackAnimation;
		private float _timeToAttackAnimationEnd;
		private int _currentAttackTriggerHash;
		
        private void Start()
		{
			_facing = GetComponent<Facing>();
			Data = GetComponent<EnvironmentData>();
			_animator = GetComponent<Animator>();
		}
		public void UpdateAttackTarget(PlayerMovement.TargetType type)
		{
			if (type != PlayerMovement.TargetType.ENEMY && Data.CurrentTarget != null) Data.SetTarget(null);
		}
		public void OnTargetInsideZone(Entity target)
		{
			Data.SetTarget(target);
			_facing.SetSightDirection(target.transform.position.x < transform.position.x ? Facing.SightDirection.LEFT : Facing.SightDirection.RIGHT);
		}
		public void OnTargetLeftZone()
		{
			if(Data.CurrentTarget != null)
			{
				ResetAttackTimer();
				Data.SetTarget(null);
			}
		}
		protected override void RuntimeUpdate()
		{
			if(Data.GetTarget() != null && _inAttackAnimation == false)
			{
				
				UpdateTimer();
			}

			if (_timeToAttackAnimationEnd > 0)
			{
				_timeToAttackAnimationEnd -= Time.deltaTime;
				
				if (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash != _currentAttackTriggerHash)
				{
					OnAttackAnimationInterrupt();
					
					_timeToAttackAnimationEnd = 0;
					return;
				}
				if (_timeToAttackAnimationEnd <= 0)
				{
					OnAttackAnimationEnd();
				}
			}
		}
		private void OnAttackAnimationEnd()
		{
			
		}

		private void OnAttackAnimationInterrupt()
		{
			_inAttackAnimation = false;
			Debug.LogWarning("Attack interrupt");
		}
		private void StartAttack()
		{
            if (_animator != null)
            {
	            string triggerName = ATTACK_TRIGGER[Random.Range(0, ATTACK_TRIGGER.Length)];
                _animator.SetTrigger(triggerName);
                
                if (_animator.layerCount > 1) throw new Exception("Animator has more that one animation layer");
                var state = _animator.GetCurrentAnimatorStateInfo(0);

                _currentAttackTriggerHash = state.shortNameHash;
                
                _timeToAttackAnimationEnd = state.length;
                
            }
			_inAttackAnimation = true;
			OnStartAttack(Data.CurrentTarget);
		}

		protected virtual void OnStartAttack(Transform target)
		{
			
		}

		

		protected virtual void OnPerformAttack(Transform target)
		{
			
		}
		public void PerformAttack()
		{
			OnPerformAttack(Data.CurrentTarget);

			_timeToAttackAnimationEnd = 0;
            _inAttackAnimation = false;
            ResetAttackTimer();
		}
		protected virtual void OnAttack()
		{
			if(attackParticles != null)
			{
				attackParticles.transform.position = Data.CurrentTarget.position;
				attackParticles.Play();
			}
		}
		private void UpdateTimer()
		{
				
			if (_timeSinceAttack < _timeToAttack)
			{
				_timeSinceAttack += Time.deltaTime;
			}
			else
			{
				var m = Entity as IAttackMutable;
				if ((m == null || m.MutedAttack == false)  && Entity.IsMuted == false)
				{
					if (_manualAttack)
					{
						StartAttack();
					}
					else
						PerformAttack();
				}
			}

		}
		private void ResetAttackTimer()
		{
			_timeToAttack = 1 / AttackSpeed;
			_timeSinceAttack = 0.0f;
		}
    }
}