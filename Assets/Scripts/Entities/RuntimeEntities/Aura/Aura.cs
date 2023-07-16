using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.RuntimeEntities.Aura
{
    internal abstract class Aura : BaseEntity
    {
        protected abstract EntityTypeBase TargetType { get; }
        [field: SerializeField] protected float Radius { get; private set; }
        private void Awake()
        {
            
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}
