using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    internal abstract class Entity : MonoBehaviour
    {
        public abstract EntityTypeBase ThisType { get; }
        public abstract EntityStats Stats { get; }
        protected virtual void OnDestroy()
        {
            LevelCompositeRoot.Instance.LevelInfo.UnregisterEntity(this);
        }
        protected virtual void Awake()
        {
            LevelCompositeRoot.Instance.LevelInfo.RegisterEntity(this);
        }
    }
}
