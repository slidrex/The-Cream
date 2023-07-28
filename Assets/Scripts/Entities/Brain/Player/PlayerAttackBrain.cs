using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.AI.ContextSteering;
using Assets.Scripts.Entities.Attack;
using Assets.Scripts.Entities.Movement.Interfaces;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Navigation.Pulling;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Brain.Player
{
    internal class PlayerAttackBrain : MobBaseAttack, IPullable
    {
        private PullInfoHolder _pullInfo;
        private AIData _aiData;
        private SteeringMovement _movePattern;
        protected override void Start()
        {
            base.Start();
            _aiData = GetComponent<AIData>();
            _movePattern = GetComponent<SteeringMovement>();
            _pullInfo = new PullInfoHolder(transform);
        }
        public void Pull(Transform pullZone)
        {
            _pullInfo.AddPullTarget(pullZone);
        }

        public void Revoke(Transform pullZone)
        {
            _pullInfo.TryRevoke(pullZone);
        }

        protected override void RuntimeUpdate()
        {
            Transform target;
            bool _ignoreSafeDistance = false;
            if (_pullInfo.TryGetPullTarget(out target)) 
            {
                _ignoreSafeDistance = true;
                _aiData.SetTargetTransform(target);
            }
            else
            {
                if(_aiData.CurrentTargetEntity == null)
                    _aiData.SetTarget(NavigationUtil.GetClosestEntityOfType(Entity.TargetType, transform));
            }


            if (_timeSinceAttack < _timeToAttack)
            {
                _timeSinceAttack += Time.deltaTime;
            }
            else
            {
                var targetEntity = _aiData.CurrentTargetEntity;
                if (targetEntity != null && targetEntity is IDamageable && _aiData.IsReachedTarget) Attack(targetEntity);
            }
            StalkTarget(_aiData.CurrentTarget);
            if (_ignoreSafeDistance == false)
                Movement.SetMoveDirection(_movePattern.GetDirectionToMove());
            else Movement.SetMoveDirection((_aiData.CurrentTarget.position - transform.position).normalized);
        }

    }
}
