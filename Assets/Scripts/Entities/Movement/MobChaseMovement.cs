using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Navigator;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement.Mob
{
    internal sealed class MobChaseMovement : EntityMovement
    {
        [Tooltip("Will attack any entity with target tags if you leave it empty"), SerializeField] private PlayerTag[] _targetTags;
        [SerializeField] private Navigator _navigator;
        private Entity _target;
        protected override void LevelRunningUpdate()
        {
            if(_target == null) _target = _navigator.GetNearestTarget();

            if(_target != null)
            {
                if (_navigator.IsTargetValid(_target.transform))
                {
                    _navigator.SetTarget(_target);
                    float targetDistSqr = Vector2.SqrMagnitude(_target.transform.position - transform.position);
                    float dist = SafeDistance.SafeDistance;
                    if(targetDistSqr > dist * dist)
                        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * Stats.CurrentSpeed);
                }
                else _target = null;
            }
        }
    }
}
