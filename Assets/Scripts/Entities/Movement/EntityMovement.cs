using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement
{
    internal abstract class EntityMovement : MonoBehaviour, ILevelRunHandler
    {
        public Entity AttachedEntity { get; private set; }
        protected IMoveable Stats;
        private bool _isRunning;
        protected IMovementSafeDistanceProvider SafeDistance { get; private set; }
        private void Update()
        {
            if (_isRunning) LevelRunningUpdate();
        }
        protected virtual void Start()
        {
            var entity = GetComponent<Entity>();
            SafeDistance = GetComponent<IMovementSafeDistanceProvider>();

            AttachedEntity = entity;
            Stats = entity as IMoveable;
            if (Stats == null) throw new Exception("Entity doesn't have IMoveable attribute");
            _isRunning = LevelCompositeRoot.Instance.Runner.IsLevelRunning;
            OnAfterStart();
        }
        protected virtual void OnAfterStart()
        {

        }
        protected virtual void LevelRunningUpdate()
        {

        }
        protected bool IsInsideSafeZone(Transform target)
        {
            float dist = SafeDistance.SafeDistance;
            return Vector2.SqrMagnitude(target.position - AttachedEntity.transform.position) <= dist * dist;
        }
        public void OnLevelRun(bool run)
        {
            _isRunning = run;
        }
    }
}
