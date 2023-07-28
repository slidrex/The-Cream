using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.StatAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Structures.Structures.BuffStructures
{
    internal class FountainEntity : EditorConstructEntity
    {
        public override EntityTypeBase ThisType { get; } = new EntityType<StructureTag>(StructureTag.BUFF);
        public override EntityTypeBase TargetType { get; } = new EntityType<PlayerTag>(PlayerTag.PLAYER);

        public override AttributeHolder Stats => new();

        public override byte SpaceRequired => 4;
        public override void OnReset()
        {

        }
    }
}
