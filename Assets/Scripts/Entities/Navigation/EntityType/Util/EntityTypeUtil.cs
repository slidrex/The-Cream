using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entities.Navigation.EntityType.Util
{
    internal static class EntityTypeUtil
    {
        public static bool ContainsEntityOfTypeInRadius<EntityType>(Vector2 origin, float radius) where EntityType : Enum
        {
            var colliders = Physics2D.OverlapCircleAll(origin, radius);
            foreach(var collider in colliders)
            {
                if (collider.TryGetComponent<Entity>(out var entity) && entity.ThisType is EntityType<EntityType>) return true;
            }
            return false;
        }
        public static List<Entity> GetEntitiesOfTypeInRadius<EntityType>(Vector2 origin, float radius) where EntityType : Enum
        {
            List<Entity> result = new();

            var entities = Physics2D.OverlapCircleAll(origin, radius).Select(x => x.GetComponent<Entity>()).NotNull();
            
            foreach ( var entity in entities)
            {
                if(entity.ThisType as EntityType<EntityType> != null)
                {
                    result.Add(entity);
                }
            }
            return result;
        }
    }
}
