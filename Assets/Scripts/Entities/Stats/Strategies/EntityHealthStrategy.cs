﻿using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.EntityExperienceLevel;
using Assets.Scripts.Entities.Mobs.Loot;
using Assets.Scripts.Entities.Stats.Interfaces;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.States;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Sound;
using Assets.Scripts.Sound.Entity;
using Assets.Scripts.Sound.Soundtrack;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats.Strategies
{
    internal class EntityHealthStrategy
    {
        public static void Damage(Entity entity, int damage, Entity dealer)
        {
            IDamageable damageable = entity as IDamageable;
            IHealthChangedHandler changeHealthHandler = entity as IHealthChangedHandler;
            IInvulnerable invulnerable = entity as IInvulnerable;

            if (invulnerable != null && invulnerable.IsInvulnerable) return;
            if(entity is IDamageCorrector corrector)
            {
                if (corrector.Masks == null) corrector.Masks = new();
                corrector.OnDamageIncomed?.Invoke(damage);
                int additional = 0;
                int multiplier = 1;
                foreach(var layer in corrector.Masks)
                {
                    if (layer.Operation == AdjustmentOperation.ADD) additional += layer.Value;
                    else multiplier *= layer.Value;
                }
                damage = (damage + additional) * multiplier;
            }

            int maxHealth = entity.Stats.GetValueInt<MaxHealthStat>();
            int oldHealth = damageable.CurrentHealth;
            
            
            damageable.CurrentHealth = Mathf.Clamp(damageable.CurrentHealth - damage, 0, maxHealth);
            changeHealthHandler?.OnHealthChanged?.Invoke(oldHealth, damageable.CurrentHealth, dealer);
            if(entity as Assets.Scripts.Entities.Player.Player == null)
            SoundCompositeRoot.Instance.SoundPlayer.Play(entity.transform.position, SoundCompositeRoot.Instance.SoundEffectStorage.EntityHitSound);

            
            if(damageable.CurrentHealth == 0 && damageable.IsDead == false)
            {
                damageable.IsDead = true;
                if(dealer != null && dealer is IKillCatcher killCatcher) 
                {
                    killCatcher.OnKill();
                    killCatcher.OnKillCallback?.Invoke();
                }
                LevelCompositeRoot.Instance.LevelInfo.OnEntityDie.Invoke(entity);
                if (entity is IExperienceGainer incomingGain && dealer is ILevelEntity exp) exp.AddExperience(incomingGain.OnDieExp);
                if (entity is IDieSoundPlayer soundPlayer && soundPlayer.OnDieSound != null) SoundCompositeRoot.Instance.SoundPlayer.Play(entity.transform.position, soundPlayer.OnDieSound);
                damageable.OnDie();
                if(entity.TryGetComponent<LootTable>(out var table))
                {
                    table.DropLoot();
                }
            }
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDamaged.Invoke();
        }
        public static void Heal(Entity entity, int heal)
        {
            IDamageable damageable = entity as IDamageable;
            IHealthChangedHandler changeHealthHandler = entity as IHealthChangedHandler;
            int maxHealth = entity.Stats.GetValueInt<MaxHealthStat>();
            int oldHealth = damageable.CurrentHealth;
            damageable.CurrentHealth = Mathf.Clamp(damageable.CurrentHealth + heal, 0, maxHealth);
            changeHealthHandler?.OnHealthChanged.Invoke(oldHealth, damageable.CurrentHealth, null);

        }
        public static void SetHealth(Entity entity, int health) 
        {
            var damageable = entity as IDamageable;
            var changeHealthHandler = entity as IHealthChangedHandler;
            int oldHealth = damageable.CurrentHealth;
            damageable.CurrentHealth = health;

            if (changeHealthHandler != null)
                changeHealthHandler.OnHealthChanged?.Invoke(oldHealth, damageable.CurrentHealth, null);
        }
        public static void ResetHealth(Entity entity)
        {
            IDamageable damageable = entity as IDamageable;
            IHealthChangedHandler changeHealthHandler = entity as IHealthChangedHandler;
            int maxHealth = entity.Stats.GetValueInt<MaxHealthStat>();
            int oldHealth = damageable.CurrentHealth;
            damageable.CurrentHealth = maxHealth;
            changeHealthHandler?.OnHealthChanged?.Invoke(oldHealth, maxHealth, null);
        }
    }
}
