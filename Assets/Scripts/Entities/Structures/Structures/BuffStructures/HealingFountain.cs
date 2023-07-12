using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Structures.Structures
{
    internal class HealingFountain : Entity
    {
        [UnityEngine.SerializeField] private float _radius;
        public override EntityTypeBase ThisType => new EntityType<StructureTag>(StructureTag.BUFF);

        public override EntityStats Stats => new HealingFountainStats(_radius, 0.2f);
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
