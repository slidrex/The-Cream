using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.Interfaces;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Structures.Structures
{
    internal class HealingFountain : Entity, ICanHeal
    {
        public override EntityTypeBase ThisType => new EntityType<StructureTag>(StructureTag.BUFF);
        public float HealPercent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
