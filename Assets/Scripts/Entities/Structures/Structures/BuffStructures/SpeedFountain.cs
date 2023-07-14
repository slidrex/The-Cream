using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Scripts.Entities.Structures.Structures.BuffStructures
{
    internal class SpeedFountain : Entity
    {
        public override EntityTypeBase ThisType => new EntityType<StructureTag>(StructureTag.BUFF);
    }
}
