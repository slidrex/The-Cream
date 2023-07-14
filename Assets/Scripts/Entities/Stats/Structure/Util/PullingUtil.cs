using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.Entities.Stats.Structure.Util
{
    internal class PullingUtil
    {
        private Action<Entity, bool> _reg;
        public struct PullableEntity
        {
            public IPullable Pullable;
            public Entity Entity;
            public PullableEntity(Entity entity, IPullable pullable)
            {
                Entity = entity;
                Pullable = pullable;
            }
        }
        private List<PullableEntity> _targets = new();
        public void OnStart(Action<Entity, bool> reg)
        {
            LevelCompositeRoot.Instance.LevelInfo.OnRegisterSubscribeAndCallOnExist(reg);
            _reg = reg;
        }
        public void OnEnd()
        {
            LevelCompositeRoot.Instance.LevelInfo.OnRegisterUnsubscribe(_reg);
        }
        public bool TryRegister(Entity entity, bool register, out PullableEntity par)
        {
            par = default(PullableEntity);
            if (register && entity.TryGetComponent<IPullable>(out var pullable))
            {
                var e = new PullableEntity(entity, pullable);
                par = e;
                _targets.Add(e);
                return true;
            }
            else if (!register) _targets.Remove(_targets.FirstOrDefault(x => x.Entity == entity));
            return false;
        }
        public List<PullableEntity> GetTargets() => _targets;
        public void PullAll(Transform transform, Predicate<PullableEntity> predicate)
        {
            foreach(var target in GetTargets()) if(predicate(target)) target.Pullable.Pull(transform);
        }
        public void PullAll(Transform transform)
        {
            foreach (var target in GetTargets()) target.Pullable.Pull(transform);
        }
    }
}
