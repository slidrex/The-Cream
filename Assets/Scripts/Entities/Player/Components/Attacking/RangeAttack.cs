using Assets.Scripts.Entities.Projectiles;
using UnityEngine;

namespace Entities.Player.Components.Attacking
{
    internal class RangeAttack : PlayerAttack
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Projectile _projectile;
        protected override void OnPerformAttack(Transform target)
        {
            if(target == null) return;
            var obj = Instantiate(_projectile, _shootPoint.position, Quaternion.identity);
            obj.SetMoveDirection((target.transform.position - transform.position).normalized);
            
        }
        protected override void OnStartAttack(Transform target)
        {
            
        }
    }
}