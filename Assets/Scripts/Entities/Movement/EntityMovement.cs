using Assets.Scripts.CompositeRoots;
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
        public Entity AttachedEntity;
        protected IMoveable Stats;
        private bool _isRunning;
        private void Update()
        {
            if (_isRunning) LevelRunningUpdate();
        }
        private void Start()
        {
            var entity = GetComponent<Entity>();
            AttachedEntity = entity;
            Stats = entity.Stats as IMoveable;
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

        public void OnLevelRun(bool run)
        {
            _isRunning = run;
        }
    }
}
