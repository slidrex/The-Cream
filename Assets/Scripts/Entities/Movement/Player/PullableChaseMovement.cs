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
    internal abstract class PullableChaseMovement<TargetType> : EntityMovement, IPullable where TargetType : Enum
    {
        protected abstract EntityType<TargetType> _targetTypes { get; }
        private EntityNavigator<TargetType> _navigator;
        private PullInfoHolder _pullInfo;
        protected override void Start()
        {
            _navigator = new EntityNavigator<TargetType>(_targetTypes);
            _pullInfo = new PullInfoHolder(transform);
            base.Start();
            _navigator.AssignEntity(AttachedEntity);
        }
        protected override void LevelRunningUpdate()
        {
            Transform target;
            bool isTargetPullZone = false;
            if (_pullInfo.TryGetPullTarget(out target)) { isTargetPullZone = true; }
            else target = _navigator.GetNearestTarget()?.transform;
            if(target != null && (isTargetPullZone || IsInsideSafeZone(target) == false))
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Stats.CurrentSpeed * Time.deltaTime);
            }
        }

        public void Pull(Transform pullZone)
        {
            _pullInfo.AddPullTarget(pullZone);
        }
    }
}
