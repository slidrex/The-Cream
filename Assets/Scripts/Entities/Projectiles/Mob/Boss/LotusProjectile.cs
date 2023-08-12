using Assets.Scripts.Entities.Mobs.Bosses.Lotus;
using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Projectiles.Mob.Boss
{
	internal class LotusProjectile : AttackProjectile<Lotus>
	{
		public override EntityTypeBase TriggerEntityType =>  new EntityType<PlayerTag>().Any();
		protected override void OnTargetHit(Entity hit)
		{
			Destroy(gameObject);
		}
        protected override void OnRuntimeRoundChanged()
        {
			Destroy(gameObject);
        }
    }
}
