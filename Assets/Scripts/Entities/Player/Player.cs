using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Player
{
    internal class Player : Entity
    {
        public override EntityStats Stats => new PlayerStats(100, 2.3f, 1.5f);

        public override EntityTypeBase ThisType => new EntityType<PlayerTag>(PlayerTag.PLAYER);
    }
}
