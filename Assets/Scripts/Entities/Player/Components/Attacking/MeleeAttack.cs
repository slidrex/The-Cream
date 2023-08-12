﻿using Assets.Scripts.Entities.AI.ContextSteering;
using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.Entities.Player.Components.Attacking
{
	internal class MeleeAttack : EntityBrain<Player>
	{
		[SerializeField] private ParticleSystem attackParticles;
		[SerializeField] private string[] ATTACK_TRIGGER = new string[2] { "Attack", "AlternativeAttack" };
		private float AttackSpeed { get => Entity.Stats.GetValue<AttackSpeedStat>(); }
		private int Damage { get => Entity.Stats.GetValueInt<DamageStat>(); }
		private Facing _facing;
		private EnvironmentData _data;
		private float _timeToAttack;
		private float _timeSinceAttack;
		private Animator _animator;
        private void Start()
		{
			_facing = GetComponent<Facing>();
			_data = GetComponent<EnvironmentData>();
			_animator = GetComponent<Animator>();
		}
		public void UpdateAttackTarget(Assets.Scripts.Entities.Player.Moving.PlayerMovement.TargetType type)
		{
			if (type != Moving.PlayerMovement.TargetType.ENEMY && _data.CurrentTarget != null) _data.SetTarget(null);
		}
		public void OnTargetInsideZone(Entity target)
		{
			_data.SetTarget(target);
			_facing.SetSightDirection(target.transform.position.x < transform.position.x ? Facing.SightDirection.LEFT : Facing.SightDirection.RIGHT);
		}
		public void OnTargetLeftZone()
		{
			if(_data.CurrentTarget != null)
			{
				ResetAttackTimer();
				_data.SetTarget(null);
			}
		}
		protected override void RuntimeUpdate()
		{
			if(_data.CurrentTargetEntity != null)
			{
				UpdateTimer();
			}
		}
		private void Attack()
		{
			if (_data.CurrentTargetEntity is IDamageable d) d.Damage(Damage, Entity);

			ResetAttackTimer();
			OnAttack();
		}
		protected virtual void OnAttack()
		{
			if (_animator != null)
			{
				_animator.SetTrigger(ATTACK_TRIGGER[UnityEngine.Random.Range(0, ATTACK_TRIGGER.Length)]);

			}
			if(attackParticles != null)
			{
				attackParticles.transform.position = _data.CurrentTarget.position;
				attackParticles.Play();
			}
		}
		private void UpdateTimer()
		{
			if (_timeSinceAttack < _timeToAttack)
			{
				_timeSinceAttack += Time.deltaTime;
			}
			else Attack();

		}
		private void ResetAttackTimer()
		{
			_timeToAttack = 1 / AttackSpeed;
			_timeSinceAttack = 0.0f;
		}

	}
}
