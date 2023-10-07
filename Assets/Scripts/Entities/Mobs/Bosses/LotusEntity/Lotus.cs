using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.States;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.Strategies;
using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Mobs.Bosses.Lotus
{
	internal class Lotus : Entity, IDamageable, IHealthChangedHandler, IInvulnerable
	{
		[SerializeField] private VictoryEvent onDieEvent;
		[SerializeField] private ParticleSystem onDamage;
		public override EntityTypeBase ThisType => new EntityType<MobTag>().Any();

		public override EntityTypeBase TargetType => new EntityType<PlayerTag>().Any();

		public override AttributeHolder Stats => new(new MaxHealthStat(70), new AttackSpeedStat(1), new DamageStat(1));

		public int CurrentHealth { get; set; }
		public Action<int, int, Entity> OnHealthChanged { get; set; }

		public bool IsInvulnerable { get; set; }
        public bool IsDead { get; set; }

        public void Damage(int damage, Entity dealer)
		{
			EntityHealthStrategy.Damage(this, damage, dealer);
			onDamage.Play();

        }

		public void Heal(int heal)
		{
			EntityHealthStrategy.Heal(this, heal);
		}

		public void OnDie()
		{
			Instantiate(onDieEvent, transform.position, UnityEngine.Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
