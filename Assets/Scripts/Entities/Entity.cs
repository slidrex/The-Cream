using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(StatModifierHandler))]
    internal abstract class Entity : MonoBehaviour
    {
        public abstract EntityTypeBase ThisType { get; }
        public abstract EntityStats Stats { get; }
        public StatModifierHandler StatModifierHandler { get; private set; }
        protected virtual void OnDestroy()
        {
            LevelCompositeRoot.Instance.LevelInfo.UnregisterEntity(this);
        }
        protected virtual void Awake()
        {
            StatModifierHandler = GetComponent<StatModifierHandler>();
            LevelCompositeRoot.Instance.LevelInfo.RegisterEntity(this);
        }
    }
}
