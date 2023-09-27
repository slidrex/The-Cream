using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.EntityType.Util;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats.Structure.Aura
{
    internal abstract class AuraStructure<TargetEntityType> : MonoBehaviour where TargetEntityType : Enum
    {
        [SerializeField] private float _activateInterval;
        private float _timeSinceActivate;
        protected bool IsReady { get; private set; }
        protected bool _isRunning;
        [SerializeField] private float _targetRadius;
        protected virtual void OnActivateEntityTypeInsideAuraAndReady(List<Entity> entitiesOfActivateType)
        {

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
            if (IsReady) OnReadyUpdate();
        }
        private void OnReadyUpdate()
        {
            List<Entity> listOfActivateEntities = EntityTypeUtil.GetEntitiesOfTypeInRadius<TargetEntityType>(transform.position, _targetRadius);
            
            if (listOfActivateEntities.Count > 0) OnActivateEntityTypeInsideAuraAndReady(listOfActivateEntities);
        }
        protected void Update()
        {
            if (_isRunning)
            {
                OnLevelRunUpdate();
                LevelRunningUpdate();
            }
        }
        protected virtual void OnAuraBecomeReady() { }
        protected bool TryActivate()
        {
            if(_timeSinceActivate >= _activateInterval)
            {
                _timeSinceActivate = 0;
                IsReady = false;
                OnActivate(Physics2D.OverlapCircleAll(transform.position, _targetRadius).Select(x => x.GetComponent<Entity>()).Where(x => x != null).ToArray());
                return true;
            }
            return false;
        }
        protected virtual void OnActivate(Entity[] entitiesInRadius)
        {

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _targetRadius);
        }
        private void OnLevelModChanged(GameMode gameMode)
        {
            _isRunning = gameMode == GameMode.RUNTIME;
        }

        private void OnEnable()
        {
            _isRunning = Editor.Editor.Instance.CurrentGamemode == GameMode.RUNTIME;
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnLevelModChanged;
        }
        private void OnDisable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnLevelModChanged;
        }
    }
}
