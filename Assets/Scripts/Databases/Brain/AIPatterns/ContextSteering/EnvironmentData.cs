using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Stats.Interfaces.Detect;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering
{
    internal sealed class EnvironmentData : MonoBehaviour, ILevelRunHandler
    {
        public Collider2D[] Obstacles { get; private set; }
        public Transform CurrentTarget { get; private set; }
        private Entity _currentTargetEntity;
        public Collider2D TargetCollider { get; private set; }
        public bool IsReachedTarget { get; set; }
        private void OnEnable() => LevelCompositeRoot.Instance.LevelInfo.OnRegisterSubscribeAndCallOnExist(OnEntitySpawned);
        private void OnDisable() => LevelCompositeRoot.Instance.LevelInfo.OnRegisterUnsubscribe(OnEntitySpawned);
        private void LoadBasicData()
        {
            Obstacles = FindObjectsOfType<Collider2D>().Where(x => x.GetInstanceID() != GetInstanceID()).ToArray();
        }
        public Entity GetTarget()
        {
            if (_currentTargetEntity != null && _currentTargetEntity is IUndetectable d && d.IsUndetectable) SetTarget(null);
            return _currentTargetEntity;
        }
        public void SetTarget(Entity entity)
        {
            _currentTargetEntity = entity;
            if (entity != null)
            {
                CurrentTarget = entity.transform;
                TargetCollider = entity.GetComponent<Collider2D>();
            }
            else
            {
                CurrentTarget = null;
                TargetCollider = null;
            }
            
        }
        private void OnEntitySpawned(Entity entity, bool spawn) => LoadBasicData();
        public void OnLevelRun(bool run) => LoadBasicData();
    }
}
