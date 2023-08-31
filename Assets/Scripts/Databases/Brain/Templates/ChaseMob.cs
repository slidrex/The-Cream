﻿using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.Templates;
using Assets.Scripts.Entities.Stats.Strategies;

namespace Assets.Scripts.Entities.Templates
{
    internal abstract class ChaseMob : Mob, IBaseMobStatsProvider, IDamageable
    {
        public override EntityTypeBase ThisType => new EntityType<MobTag>(MobTag.AGGRESSIVE);
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }

        public void Damage(int damage, Entity deler)
        {
            EntityHealthStrategy.Damage(this, damage, deler);
        }

        public void Heal(int heal)
        {
            EntityHealthStrategy.Heal(this, heal);
        }

        public virtual void OnDie()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            EntityHealthStrategy.ResetHealth(this);
        }
    }
}