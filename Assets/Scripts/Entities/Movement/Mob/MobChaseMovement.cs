using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Navigator;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement.Mob
{
    internal class MobChaseMovement : EntityMovement
    {
        private EntityNavigator<PlayerTag> _navigator = new(new EntityType<PlayerTag>().Any());
        private Transform _target;
        protected override void OnAfterStart()
        {
            _navigator.AssignEntity(AttachedEntity);
        }
        protected override void LevelRunningUpdate()
        {
            if(_target == null) _target = _navigator.GetNearestTarget()?.transform;

            if(_target != null)
            {
                if (_navigator.IsTargetValid(_target))
                {
                    transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * Stats.CurrentSpeed);
                }
                else _target = null;
            }
        }
    }
}
