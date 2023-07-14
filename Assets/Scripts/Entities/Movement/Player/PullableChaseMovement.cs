using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Navigation.Navigator;
using Assets.Scripts.Entities.Navigation.Pulling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement.Player
{
    internal class PullableChaseMovement : EntityMovement, IPullable
    {
        [SerializeField] private Navigator _navigator;
        private PullInfoHolder _pullInfo;
        protected override void Start()
        {
            _pullInfo = new PullInfoHolder(transform);
            base.Start();
        }
        protected override void LevelRunningUpdate()
        {
            Transform target;
            bool isTargetPullZone = false;
            if (_pullInfo.TryGetPullTarget(out target)) { isTargetPullZone = true; }
            else
            {
                var targ = _navigator.GetNearestTarget();
                target = targ != null ? targ.transform : null;
                _navigator.SetTarget(targ);
            }
            if(target != null && (isTargetPullZone || IsInsideSafeZone(target) == false))
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Stats.CurrentSpeed * Time.deltaTime);
            }
        }

        public void Pull(Transform pullZone)
        {
            _pullInfo.AddPullTarget(pullZone);
        }

        public void Revoke(Transform pullZone)
        {
            _pullInfo.TryRevoke(pullZone);
        }
    }
}
