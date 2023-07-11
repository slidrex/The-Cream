using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Target;
using Assets.Scripts.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entities.Navigation.Navigator
{
    internal class EntityNavigator<TargetType> where TargetType : Enum
    {
        private const int NOT_ASSIGNED = -1;
        private Entity _entity;
        private EntityType<TargetType> _targets;
        private float _targetFindRadius;
        public EntityNavigator(EntityType<TargetType> targets, float targetFindRadius = NOT_ASSIGNED) 
        {
            _targetFindRadius = targetFindRadius;
            _targets = targets;
        }
        public void AssignEntity(Entity entity)
        {
            _entity = entity;
        }
        public Entity GetNearestTarget()
        {
            var entitites = _targetFindRadius == NOT_ASSIGNED ? LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities : Physics2D.OverlapCircleAll(_entity.transform.position, _targetFindRadius).Select(x => x.GetComponent<Entity>()).NotNull();
            List<Entity> potentialEntities = new();
            

            foreach(var entity in entitites)
            {
                if (entity.GetInstanceID() != _entity.GetInstanceID() && entity.ThisType is EntityType<TargetType> entityType)
                {
                    if (IsEntityTarget(entityType))
                    {
                        potentialEntities.Add(entity);
                    }
                }
            }
            var nearest = Mathc.GetNearestTo(_entity, potentialEntities);
            return nearest;
        }
        private bool IsEntityTarget(EntityType<TargetType> targetTypes)
        {
            var thisTargetTypes = _targets.GetTags();
            if (targetTypes.IsAny) return true;
            foreach(var thisType in thisTargetTypes)
            {
                foreach(var target in targetTypes.GetTags())
                {
                    if(target.ToString() == thisType.ToString()) return true;
                }
            }
            return false;
        }
        public bool IsTargetValid(Transform target)
        {
            return _targetFindRadius == NOT_ASSIGNED || Vector2.SqrMagnitude(target.position - _entity.transform.position) <= _targetFindRadius * _targetFindRadius;
        }
    }
}
