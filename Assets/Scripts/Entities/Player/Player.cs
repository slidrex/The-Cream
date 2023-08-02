using Assets.Scripts.Entities.EntityExperienceLevel;
using Assets.Scripts.Entities.EntityExperienceLevel.UI;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Entities.Stats.Interfaces;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.States;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.Strategies;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Entities.Player
{
    internal class Player : Entity, IDamageable, ILevelEntity, IResettable, IHealthChangedHandler, IStatic, IDamageCorrector
    {
        private EntityLevelBar _levelBar;
        public override EntityTypeBase ThisType => new EntityType<PlayerTag>(PlayerTag.PLAYER);
        public override EntityTypeBase TargetType => new EntityType<MobTag>().Any();
        public int CurrentHealth { get; set; }
        public Action<int, int, Entity> OnHealthChanged { get; set; }
        public byte CurrentLevel { get; set; }
        public int CurrentExp { get; set; }

        public override AttributeHolder Stats { get; } = new AttributeHolder(new MaxHealthStat(100), new SpeedStat(2), new DamageStat(5), new AttackSpeedStat(1));
        public int MaxHealth { get; }
        public List<AdjustmentMask> Masks { get; set; }
        public Action<int> OnDamageIncomed { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _levelBar = GetComponentInChildren<EntityLevelBar>();
        }
        public void OnReset()
        {
            EntityHealthStrategy.ResetHealth(this);
        }
        public void AddExperience(int exp)
        {
            EntityExperienceLevel.Strategy.EntityLevelStrategy.AddExperience(exp, this);
            _levelBar.UpdateBar(this);
        }

        public void Damage(int damage, Entity dealer) => EntityHealthStrategy.Damage(this, damage, dealer);

        public int GetLevelExperienceCost(int level) => 3 * (level + 1);

        public void Heal(int heal) => EntityHealthStrategy.Heal(this, heal);

        public void OnDie() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public virtual void OnLevelUp()
        {

        }

    }
}
