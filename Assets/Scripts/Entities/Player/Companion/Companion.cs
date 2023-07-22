using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Player.Companion
{
    internal class Companion : Entity
    {
        public override EntityTypeBase ThisType => new EntityType<PlayerTag>(PlayerTag.COMPANION);

        public override EntityTypeBase TargetType => new EntityType<MobTag>().Any();

        public override AttributeHolder Stats => throw new NotImplementedException();
    }
}
