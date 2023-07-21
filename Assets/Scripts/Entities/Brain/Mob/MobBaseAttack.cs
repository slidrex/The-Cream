﻿using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Brain;
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
    [RequireComponent (typeof(Navigator))]
    [RequireComponent(typeof (EntityMovement))]
    internal class MobBaseAttack : EntityBrain
    {
        [SerializeField] private float _attackDistance;
        private ICanDamage _attackComponent;
        protected float _timeSinceAttack;
        protected float _timeToAttack;

        protected Navigator Navigator;
        protected EntityMovement Movement;

        protected virtual void Start()
        {
            Movement = GetComponent<EntityMovement>();
            Navigator = GetComponent<Navigator>();
            Movement.SetSafeDistance(_attackDistance);

            _attackComponent = Entity as ICanDamage;
            if (_attackComponent == null) throw new NullReferenceException("Entity " + name + " doesn't contain ICanDamage interface");

            ResetAttackTimer();
        }
        protected override void RuntimeUpdate()
        {
            if (Navigator.GetTargetTransform() == null)
                Navigator.SetTarget(Navigator.GetNearestTargetEntity());

            if (_timeSinceAttack < _timeToAttack)
            {
                _timeSinceAttack += Time.deltaTime;
            }
            else
            {
                var target = Navigator.GetTarget();
                if (target != null && target is IDamageable && Movement.IsInsideSafeDistance(target.transform)) Attack(target);
            }
            Movement.MoveToTarget(stopIfSafeDistance: true);
        }
        protected void Attack(Entity target)
        {
            ResetAttackTimer();
            var damageable = target as IDamageable;
            damageable.Damage(_attackComponent.AttackDamage, Entity);
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