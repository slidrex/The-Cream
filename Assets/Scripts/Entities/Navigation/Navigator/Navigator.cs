using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Navigation.Util;
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
    internal sealed class Navigator : MonoBehaviour, INavigator
    {
        private const int NOT_ASSIGNED = 0;
        private Entity _target;
        private Transform _targetTransform;
        private float _targetFindRadius { get; set; }
        private Entity _entity;
        private EntityTypeBase _targets;
        private void Awake()
        {
            _entity = GetComponent<Entity>();
            _targetFindRadius = _entity is IHaveTargetFindRadius rad ? rad.TargetFindRadius : NOT_ASSIGNED;
            _targets = _entity.TargetType;
        }
        public Entity GetNearestTargetEntity() => Mathc.GetNearestTo(_entity, GetTargets());
        public List<Entity> GetTargets()
        {
            return NavigationUtil.GetEntitiesOfTypeInsideOriginTile(_targets, _entity, _targetFindRadius);
        }
        public void SetTarget(Entity target)
        {
            _target = target;
            if(target != null)
                _targetTransform = target.transform;
        }
        public void SetTarget(Transform target)
        {
            _targetTransform = target;
            _target = null;
        }
        public bool IsTargetInsideFindRadius(Transform target) => _targetFindRadius == NOT_ASSIGNED || Vector2.SqrMagnitude(target.position - _entity.transform.position) <= _targetFindRadius * _targetFindRadius;
        public Transform GetTargetTransform() => _targetTransform;
        public Entity GetTarget() => _target;
    }
}
