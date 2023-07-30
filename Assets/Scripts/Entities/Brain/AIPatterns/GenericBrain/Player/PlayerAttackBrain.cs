using Assets.Scripts.Entities.AI.ContextSteering;
using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Attack;
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
    [RequireComponent(typeof(Facing))]
    internal class PlayerAttackBrain : MobBaseAttack, IPullable
    {
        private PullInfoHolder _pullInfo;
        private Facing _facing;
        private AIData _aiData;
        private SteeringMovement _movePattern;
        protected override void Start()
        {
            base.Start();
            _facing = GetComponent<Facing>();
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
                    _aiData.SetTarget(NavigationUtil.GetClosestEntityOfType(Entity.TargetType, Entity));
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
            _facing.StalkTarget(_aiData.CurrentTarget);
            _movePattern.ConsiderSafeDistance = !_ignoreSafeDistance;
            Movement.SetMoveDirection(_movePattern.GetDirectionToMove());
        }

    }
}
