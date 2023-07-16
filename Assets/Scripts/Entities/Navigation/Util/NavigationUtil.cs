using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entities.Navigation.Util
{
    internal class NavigationUtil
    {
        public const float NOT_ASSIGNED = 0;
        public static List<Entity> GetEntitiesOfType(EntityTypeBase type, Transform origin, float radius = NOT_ASSIGNED)
        {
            List<Entity> potentialEntities = new();
            var entitites = radius == 0 ? LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities : Physics2D.OverlapCircleAll(origin.position, radius).Select(x => x.GetComponent<Entity>()).NotNull();
            
            if (entitites != null)
                foreach (var entity in entitites)
                {
                    if (entity.GetInstanceID() != origin.GetInstanceID()
                            && type.MatchesEntityType(entity))
                    {
                        potentialEntities.Add(entity);
                    }
                }
            return potentialEntities;
        }
    }
}
