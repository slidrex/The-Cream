using Assets.Scripts.Entities.EntityExperienceLevel;
using Assets.Scripts.Entities.EntityExperienceLevel.Strategy;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Strategies
{
    internal class EntityBaseStrategy
    {
        /// <summary>
        /// Provides base functions:
        /// Reset health
        /// Reset awake
        /// </summary>
        /// <param name="entity"></param>
        public static void OnReset(Entity entity)
        {
            if(entity is IDamageable) EntityHealthStrategy.ResetHealth(entity);
            entity.transform.position = entity.AwakePosition;
        }
    }
}
