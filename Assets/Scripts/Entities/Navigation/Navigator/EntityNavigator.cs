using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Target;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
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
    internal class EntityNavigator<TargetType> : Navigator where TargetType : Enum
    {
        private const int NOT_ASSIGNED = 0;
        private Entity _target;
        [field: Tooltip("Leave it zero to have infinite target radius"), SerializeField] public float TargetFindRadius { get; private set; }
        [Tooltip("Leave it empty to make any tag in list targetable."), SerializeField] private TargetType[] _targetTags;
        private Entity _entity;
        private EntityType<TargetType> _targets;
        private void Awake()
        {
            _entity = GetComponent<Entity>();
            if (_targetTags.Length == 0)
                _targets = new EntityType<TargetType>().Any();
            else _targets = new EntityType<TargetType>(_targetTags);
        }
        public override Entity GetNearestTarget() => Mathc.GetNearestTo(_entity, GetTargets());
        public override List<Entity> GetTargets()
        {
            List<Entity> potentialEntities = new();
            var entitites = TargetFindRadius == NOT_ASSIGNED ? LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities : Physics2D.OverlapCircleAll(_entity.transform.position, TargetFindRadius).Select(x => x.GetComponent<Entity>()).NotNull();
            if (_entity == null) throw new NullReferenceException("Attached Entity wasn't found. Please, make sure that you've called 'Assign Entity' before using EntityNavigator.");
            if (entitites != null)
                foreach (var entity in entitites)
                {
                    if (entity.GetInstanceID() != _entity.GetInstanceID()
                            && entity.ThisType is EntityType<TargetType> entityType)
                    {
                        if (IsEntityTarget(entityType))
                        {
                            potentialEntities.Add(entity);
                        }
                    }
                }
            return potentialEntities;
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
        public override bool IsTargetValid(Transform target)
        {
            return TargetFindRadius == NOT_ASSIGNED || Vector2.SqrMagnitude(target.position - _entity.transform.position) <= TargetFindRadius * TargetFindRadius;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, TargetFindRadius);
        }

        public override void SetTarget(Entity target)
        {
            _target = target;
        }

        public override Entity GetTarget()
        {
            return _target;
        }
    }
}
