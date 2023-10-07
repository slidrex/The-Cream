using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Entities.Projectiles.Interfaces;

namespace Assets.Scripts.Entities.Projectiles
{
	internal abstract class AttackProjectile : Projectile, IOwnerable
	{
		[UnityEngine.SerializeField] private int damage;
		public Entity Owner { get; set; }
		protected override void OnTrigger(Entity trigger)
		{
			if(trigger is IDamageable d)
			{
				d.Damage(damage, Owner);
				OnTargetHit(trigger);
			}
		}
        protected virtual void OnTargetHit(Entity hit) 
		{

		}
	}
}
