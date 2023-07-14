using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Strategies;
using Assets.Scripts.Entities.Util.UIBars;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Entities.Player
{
    internal class Player : Entity, IDamageable, IMoveable, IHealthChangedHandler, ICanDamage
    {
        public override EntityTypeBase ThisType => new EntityType<PlayerTag>(PlayerTag.PLAYER);

        public int MaxHealth { get; set; } = 100;

        public int CurrentHealth { get; set; }

        public bool IsInvulnerable { get; set; }

        public float CurrentSpeed { get; set; } = 2.0f;
        public Action<int> OnHealthChanged { get; set; }
        public Action<int> OnDamage { get; set; }
        public Action<int> OnHeal { get; set; }
        public EntityType<MobTag> TargetEntityTags => new EntityType<MobTag>().Any();

        public float AttackSpeed { get; set; } = 2;
        public int AttackDamage { get; set; } = 2;

        public void Damage(int damage)
        {
            EntityHealthStrategy.Damage(this, damage);
        }

        public void Heal(int heal)
        {
            EntityHealthStrategy.Heal(this, heal);
        }

        public void OnDie()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Start()
        {
            EntityHealthStrategy.ResetHealth(this);
        }
    }
}
