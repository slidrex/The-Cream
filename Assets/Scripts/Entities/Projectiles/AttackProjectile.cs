using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Projectiles
{
	internal abstract class AttackProjectile<Owner> : Projectile where Owner : Entity
	{
		[UnityEngine.SerializeField] private int damage;
		public Owner _Owner { get; private set; }
		public void SetOwner(Owner owner)
		{
            _Owner = owner;
		}
		protected override void OnTrigger(Entity trigger)
		{
			if(trigger is IDamageable d)
			{
				d.Damage(damage, _Owner);
				OnTargetHit(trigger);
			}
		}
        protected virtual void OnTargetHit(Entity hit) 
		{

		}
	}
}
