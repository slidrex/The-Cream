using Assets.Scripts.Entities.Attack;
using Assets.Scripts.Entities.Movement.Interfaces;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Navigation.Pulling;
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
        protected override void Start()
        {
            base.Start();
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
                Navigator.SetTarget(target);
            }
            else
            {
                var targ = Navigator.GetNearestTargetEntity();
                Navigator.SetTarget(targ);
            }


            if (_timeSinceAttack < _timeToAttack)
            {
                _timeSinceAttack += Time.deltaTime;
            }
            else
            {
                var targetEntity = Navigator.GetTarget();
                if (targetEntity != null && targetEntity is IDamageable && Movement.IsInsideSafeDistance(targetEntity.transform)) Attack(targetEntity);
            }
            Movement.MoveToTarget(stopIfSafeDistance: !_ignoreSafeDistance);
        }

    }
}
