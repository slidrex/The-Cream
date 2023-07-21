using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.EntityExperienceLevel;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats.Strategies
{
    internal class EntityHealthStrategy
    {
        public static void Damage(Entity entity, int damage, Entity dealer)
        {
            IDamageable damageable = entity as IDamageable;
            IHealthChangedHandler changeHealthHandler = entity as IHealthChangedHandler;
            if (damageable.IsInvulnerable == false)
                damageable.CurrentHealth = Mathf.Clamp(damageable.CurrentHealth - damage, 0, damageable.MaxHealth);
            
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
            damageable.CurrentHealth = Mathf.Clamp(damageable.CurrentHealth + heal, 0, damageable.MaxHealth);
            changeHealthHandler?.OnHealthChanged.Invoke(damageable.CurrentHealth);

        }
        public static void ResetHealth(Entity entity)
        {
            IDamageable damageable = entity as IDamageable;
            IHealthChangedHandler changeHealthHandler = entity as IHealthChangedHandler;
            damageable.CurrentHealth = damageable.MaxHealth;
            changeHealthHandler?.OnHealthChanged?.Invoke(damageable.CurrentHealth);
        }
    }
}
