using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.States;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.Strategies;
using System;

namespace Assets.Scripts.Entities.Mobs.Bosses.Lotus
{
	internal class Lotus : Entity, IDamageable, IHealthChangedHandler, IInvulnerable
	{
		public override EntityTypeBase ThisType => new EntityType<MobTag>().Any();

		public override EntityTypeBase TargetType => new EntityType<PlayerTag>().Any();

		public override AttributeHolder Stats => new(new MaxHealthStat(200), new AttackSpeedStat(1));

		public int CurrentHealth { get; set; }
		public Action<int, int, Entity> OnHealthChanged { get; set; }

		public bool IsInvulnerable { get; set; }

		public void Damage(int damage, Entity dealer)
		{
			EntityHealthStrategy.Damage(this, damage, dealer);
		}

		public void Heal(int heal)
		{
			EntityHealthStrategy.Heal(this, heal);
		}

		public void OnDie()
		{

		}
	}
}
