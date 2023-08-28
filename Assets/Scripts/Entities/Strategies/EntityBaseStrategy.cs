using Assets.Scripts.Entities.EntityExperienceLevel;
using Assets.Scripts.Entities.EntityExperienceLevel.Strategy;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Strategies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.Strategies
{
    internal class EntityBaseStrategy
    {
        public struct ResetStats
        {
            public int BaseHP;
            public int BaseExp;
            public byte BaseLevel;
            public Vector2 AwakePosition;
            public ResetStats(Vector2 awakePosition, int baseHP, int baseExp, byte baseLevel)
            {
                AwakePosition = awakePosition;
                BaseHP = baseHP;
                BaseExp = baseExp;
                BaseLevel = baseLevel;
            }
        }
        private static Dictionary<int, ResetStats> _entities;
        public static void OnGameStart()
        {
            _entities = new Dictionary<int, ResetStats>();
        }
        public static void OnBeforeReset(Entity entity)
        {
            if(entity is IDamageable) EntityHealthStrategy.ResetHealth(entity);
            var d = entity as IDamageable;
            var l = entity as ILevelEntity;
            int instId = entity.GetInstanceID();
            var stats = new ResetStats(entity.transform.position, d == null ? -1 : d.CurrentHealth, l == null ? -1 : l.CurrentExp, (byte)(l == null ? 0 : l.CurrentLevel));
            if (!_entities.TryAdd(instId, stats)) _entities[instId] = stats;

        }
        public static void OnAfterReset(Entity entity)
        {
            int instId = entity.GetInstanceID();

            if(_entities.TryGetValue(instId, out var stats))
            {
                entity.transform.position = stats.AwakePosition;
                if (entity is IDamageable d) EntityHealthStrategy.SetHealth(entity, stats.BaseHP);
                if (entity is ILevelEntity l) EntityLevelStrategy.SetExpStatus(l, stats.BaseLevel, stats.BaseExp);
            }
        }
    }
}
