using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Movement.Mob.Dummy
{
    internal class Dummy : Mobs.Mob
    {
        public override byte SpaceRequired => 8;

        public override EntityTypeBase ThisType => new EntityType<MobTag>(MobTag.AGGRESSIVE);

        public override EntityStats Stats => new DummyStats(100, 1);
    }
}
