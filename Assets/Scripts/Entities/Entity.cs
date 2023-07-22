using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Strategies;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(Collider2D))]
    internal abstract class Entity : BaseEntity
    {
        public abstract EntityTypeBase ThisType { get; }
        public abstract EntityTypeBase TargetType { get; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Color DefaultColor { get; private set; }
        public abstract AttributeHolder Stats { get; }

        protected virtual void OnDestroy()
        {
            LevelCompositeRoot.Instance.LevelInfo.UnregisterEntity(this);
        }
        public void OnBeforeReset()
        {
            EntityBaseStrategy.OnBeforeReset(this);
        }
        public void OnAfterReset()
        {
            EntityBaseStrategy.OnAfterReset(this);
        }
        protected virtual void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            DefaultColor = SpriteRenderer.color;
            LevelCompositeRoot.Instance.LevelInfo.RegisterEntity(this);
        }
        private void Update()
        {
            Stats.OnUpdate();   
        }
    }
}
