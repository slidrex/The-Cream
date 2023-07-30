using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Projectiles.Character.Bristleback
{
    internal class Bristlespike : AttackProjectile<Player.Player>
	{
        public override EntityTypeBase TriggerEntityType => new EntityType<MobTag>().Any();
    }
}
