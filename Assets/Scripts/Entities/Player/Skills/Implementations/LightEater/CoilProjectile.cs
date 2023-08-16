using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Implementations.LightEater
{
    internal class CoilProjectile : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private int damage;
        private EntityType<MobTag> _targets = new EntityType<MobTag> ().Any();
        private Entity _dealer;
        public void SetOwner(Entity owner)
        {
            _dealer = owner;
        }
        private void Start()
        {
            var targets = NavigationUtil.GetAllEntitiesOfType(_targets, transform, _radius);
            var colliders = Physics2D.OverlapCircleAll(transform.position, _radius).Select(X => X.GetComponent<IDamageable>());
            foreach(var col in colliders)
            {
                if (col != null) col.Damage(damage, _dealer);
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
