﻿using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.EntityExperienceLevel;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats.Strategies
{
    internal class EntityHealthStrategy
    {
        public static void Damage(Entity entity, int damage, Entity dealer)
        {
            IDamageable damageable = entity as IDamageable;
            IHealthChangedHandler changeHealthHandler = entity as IHealthChangedHandler;
            int maxHealth = entity.Stats.GetValueInt<MaxHealthStat>();
            damageable.CurrentHealth = Mathf.Clamp(damageable.CurrentHealth - damage, 0, maxHealth);
            
            changeHealthHandler?.OnHealthChanged?.Invoke(damageable.CurrentHealth);
            if(damageable.CurrentHealth == 0)
            {
                LevelCompositeRoot.Instance.LevelInfo.OnEntityDie.Invoke(entity);
                if (entity is IExperienceGainer incomingGain && dealer is ILevelEntity exp) exp.AddExperience(incomingGain.OnDieExp); 
                damageable.OnDie();
            }
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDamaged.Invoke();
        }
        public static void Heal(Entity entity, int heal)
        {
            IDamageable damageable = entity as IDamageable;
            IHealthChangedHandler changeHealthHandler = entity as IHealthChangedHandler;
            int maxHealth = entity.Stats.GetValueInt<MaxHealthStat>();

            damageable.CurrentHealth = Mathf.Clamp(damageable.CurrentHealth + heal, 0, maxHealth);
            changeHealthHandler?.OnHealthChanged.Invoke(damageable.CurrentHealth);

        }
        public static void SetHealth(Entity entity, int health) 
        {
            var damageable = entity as IDamageable;
            var changeHealthHandler = entity as IHealthChangedHandler;
            damageable.CurrentHealth = health;
            if(changeHealthHandler != null)
                changeHealthHandler.OnHealthChanged?.Invoke(damageable.CurrentHealth);
        }
        public static void ResetHealth(Entity entity)
        {
            IDamageable damageable = entity as IDamageable;
            IHealthChangedHandler changeHealthHandler = entity as IHealthChangedHandler;
            int maxHealth = entity.Stats.GetValueInt<MaxHealthStat>();

            damageable.CurrentHealth = maxHealth;
            changeHealthHandler?.OnHealthChanged?.Invoke(damageable.CurrentHealth);
        }
    }
}
