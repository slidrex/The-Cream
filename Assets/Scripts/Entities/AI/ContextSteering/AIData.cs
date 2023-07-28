using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering
{
    internal sealed class AIData : MonoBehaviour, ILevelRunHandler
    {
        public Collider2D[] Obstacles { get; private set; }
        public Transform CurrentTarget { get; private set; }
        public Entity CurrentTargetEntity { get; private set; }
        public bool IsReachedTarget { get; set; }
        private void OnEnable() => LevelCompositeRoot.Instance.LevelInfo.OnRegisterSubscribeAndCallOnExist(OnEntitySpawned);
        private void OnDisable() => LevelCompositeRoot.Instance.LevelInfo.OnRegisterUnsubscribe(OnEntitySpawned);
        private void LoadBasicData()
        {
            Obstacles = FindObjectsOfType<Collider2D>().Where(x => x.GetInstanceID() != GetInstanceID()).ToArray();
        }
        public void SetTarget(Entity entity)
        {
            CurrentTargetEntity = entity;
            if(entity != null)
                CurrentTarget = entity.transform;
        }
        public void SetTargetTransform(Transform entity)
        {
            CurrentTargetEntity = null;
            CurrentTarget = entity;
        }
        private void OnEntitySpawned(Entity entity, bool spawn) => LoadBasicData();
        public void OnLevelRun(bool run) => LoadBasicData();
    }
}
