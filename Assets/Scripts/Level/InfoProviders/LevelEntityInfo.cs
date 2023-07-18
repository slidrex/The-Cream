using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level.InfoProviders
{
    internal class LevelEntityInfo :MonoBehaviour
    {
        public Entity[] StartEntities { get; private set; }
        public List<Entity> RuntimeEntities { get; private set; }
        private System.Action<Entity, bool> OnEntityRegister;
        public Action<Entity> OnEntityDie { get; set; }
        public void ConfigureLevelInfo()
        {
            RuntimeEntities = new List<Entity>();
            StartEntities = FindObjectsOfType<Entity>();
        }
        public void RegisterEntity(Entity entity)
        {
            RuntimeEntities.Add(entity);
            OnEntityRegister?.Invoke(entity, true);
        }
        public void UnregisterEntity(Entity entity)
        {
            OnEntityRegister?.Invoke(entity, false);
            RuntimeEntities.Remove(entity);
        }
        /// <summary>
        /// Also calls register action on existing entities.
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public List<Entity> OnRegisterSubscribeAndCallOnExist(System.Action<Entity,bool> register)
        {
            OnEntityRegister += register;
            foreach(Entity entity in RuntimeEntities)
            {
                register(entity, true);
            }
            return RuntimeEntities;
        }
        public void OnRegisterUnsubscribe(System.Action<Entity, bool> register)
        {
            OnEntityRegister -= register;
        }
    }
}
