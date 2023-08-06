using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Entities.RuntimeEntities.Aura
{
    internal abstract class DurationAura : Aura
    {
        private struct AffectedEntity
        {
            public Entity Entity;
            public float RemainTime;
            public float BaseRemain;
            public AffectedEntity(Entity entity, float time)
            {
                BaseRemain = time;
                Entity = entity;
                RemainTime = time;
            }
        }
        private Dictionary<int, AffectedEntity> _affectedEntities = new();
        [SerializeField] public float Duration;

        protected abstract void UpdateTargetEntityInside(Entity entity, out float blockTime);
        protected abstract void OnBlockTimeExpired(Entity entity);
        private void Awake()
        {
            Destroy(gameObject, Duration);
        }
        private void Update()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, Radius).Select(x => x.GetComponent<Entity>()).Where(x => x != null);
            foreach (var entity in colliders) 
            {
                if (TargetType.MatchesEntityType(entity))
                {
                    OnNeededEntityInside(entity);
                }
            }
            for(int i = 0; i < _affectedEntities.Count; i++)
            {
                var aeK = _affectedEntities.ElementAt(i);
                var ae = aeK.Value;
                ae.RemainTime -= Time.deltaTime;
                if (ae.RemainTime <= 0)
                {
                    OnBlockTimeExpired(ae.Entity);
                    _affectedEntities.Remove(aeK.Key);
                }
                else _affectedEntities[aeK.Key] = ae;
            }
        }
        private void OnNeededEntityInside(Entity entity)
        {
            int instanceId = entity.GetInstanceID();


            if (!_affectedEntities.ContainsKey(instanceId))
            {
                UpdateTargetEntityInside(entity, out float blockTime);
                AffectedEntity ae = new(entity, blockTime);
                _affectedEntities.Add(instanceId, ae);
            }
        }
        protected virtual void OnExpired() { }
        private void OnDestroy()
        {
            OnExpired();
            foreach(var obj in _affectedEntities.Values)
            {
                OnBlockTimeExpired(obj.Entity);
            }
        }
    }
}
