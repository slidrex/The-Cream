using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats.Structure.Aura
{
    internal class AuraStructure : MonoBehaviour, ILevelRunHandler
    {
        [SerializeField] private float _activateInterval;
        private float _timeSinceActivate;
        protected bool IsReady { get; private set; }
        protected bool _isRunning;
        private float _targetRadius;
        private void Start()
        {
            _targetRadius = (GetComponent<Entity>().Stats as IHaveTargetRadius).TargetRadius;
        }
        public void OnLevelRun(bool run)
        {
            _isRunning = run;
        }
        protected virtual void OnLevelRunUpdate()
        {

        }
        private void LevelRunningUpdate()
        {
            if(_timeSinceActivate < _activateInterval)
            {
                _timeSinceActivate += Time.deltaTime;
            }
            else if(!IsReady)
            {
                IsReady = true;
                OnAuraBecomeReady();
            }
        }
        protected void Update()
        {
            if (_isRunning)
            {
                OnLevelRunUpdate();
                LevelRunningUpdate();
            }
        }
        protected virtual void OnAuraBecomeReady()
        {

        }
        protected bool TryActivate()
        {
            if(_timeSinceActivate >= _activateInterval)
            {
                _timeSinceActivate = 0;
                IsReady = false;
                OnActivate(Physics2D.OverlapCircleAll(transform.position, _targetRadius).Select(x => x.GetComponent<Entity>()).NotNull().ToArray());
                return true;
            }
            return false;
        }
        protected virtual void OnActivate(Entity[] entitiesInRadius)
        {

        }
    }
}
