using Assets.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level.InfoProviders
{
    internal class LevelEntityInfo :MonoBehaviour
    {
        public Entity[] StartEntities { get; private set; }
        public List<Entity> RuntimeEntities { get; private set; }
        public void ConfigureLevelInfo()
        {
            RuntimeEntities = new List<Entity>();
            StartEntities = FindObjectsOfType<Entity>();
        }
        public void RegisterEntity(Entity entity)
        {
            RuntimeEntities.Add(entity);
        }
        public void UnregisterEntity(Entity entity)
        {
            RuntimeEntities.Remove(entity);
        }
    }
}
