﻿using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Entities.Navigation.Util
{
    internal class NavigationUtil
    {
        public const float NOT_ASSIGNED = 0;
        public static List<Entity> GetAllEntitiesOfType(EntityTypeBase type, Transform origin, float radius = NOT_ASSIGNED)
        {
            return GetEntitiesInRadius(x => x != null, type, origin.transform, radius);
        }
        public static List<Entity> GetEntitiesOfTypeInsideOriginTile(EntityTypeBase type, Entity origin, float radius = NOT_ASSIGNED)
        {
            return GetEntitiesInRadius(x => x.HousingElement.GetInstanceID() == origin.HousingElement.GetInstanceID(), type, origin.transform, radius);
        }
        public static Entity GetClosestEntityOfType(EntityTypeBase targetType, Entity origin)
        {
            return GetEntitiesInRadius(x => x.HousingElement.GetInstanceID() == origin.HousingElement.GetInstanceID(), targetType, origin.transform).OrderBy(x => Vector2.SqrMagnitude(x.transform.position - origin.transform.position)).FirstOrDefault();
        }
        private static List<Entity> GetEntitiesInRadius(Predicate<Entity> entities, EntityTypeBase type, Transform origin, float radius = NOT_ASSIGNED)
        {
            List<Entity> potentialEntities = new();
            var entitites = radius == 0 ? LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities : Physics2D.OverlapCircleAll(origin.transform.position, radius).Select(x => x.GetComponent<Entity>()).Where(x => x != null);

            if (entitites != null)
                foreach (var entity in entitites)
                {
                    if (entities.Invoke(entity) && entity.GetInstanceID() != origin.GetInstanceID()
                            && type.MatchesEntityType(entity))
                    {
                        potentialEntities.Add(entity);
                    }
                }
            return potentialEntities;
        }
    }
}
