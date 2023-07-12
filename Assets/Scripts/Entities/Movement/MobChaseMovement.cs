using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Navigator;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement.Mob
{
    internal class MobChaseMovement : EntityMovement
    {
        [Tooltip("Will attack any entity with target tags if you leave it empty"), SerializeField] private PlayerTag[] _targetTags;
        private EntityNavigator<PlayerTag> _navigator;
        private Transform _target;
        protected override void OnAfterStart()
        {
            if (_targetTags.Length == 0) _navigator = new(new EntityType<PlayerTag>().Any());
            else _navigator = new(new EntityType<PlayerTag>(_targetTags));
            _navigator.AssignEntity(AttachedEntity);
        }
        protected override void LevelRunningUpdate()
        {
            if(_target == null) _target = _navigator.GetNearestTarget()?.transform;

            if(_target != null)
            {
                if (_navigator.IsTargetValid(_target))
                {
                    float targetDistSqr = Vector2.SqrMagnitude(_target.transform.position - transform.position);
                    if(targetDistSqr > SafeDistance * SafeDistance)
                        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * Stats.CurrentSpeed);
                }
                else _target = null;
            }
        }
    }
}
