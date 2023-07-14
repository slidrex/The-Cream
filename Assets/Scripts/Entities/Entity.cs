using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(StatModifierHandler), typeof(Collider2D))]
    internal abstract class Entity : MonoBehaviour
    {
        protected SpriteRenderer SpriteRenderer { get; private set; }
        public Color DefaultColor { get; private set; }
        public abstract EntityTypeBase ThisType { get; }
        public StatModifierHandler StatModifierHandler { get; private set; }
        protected virtual void OnDestroy()
        {
            LevelCompositeRoot.Instance.LevelInfo.UnregisterEntity(this);
        }
        protected virtual void Awake()
        {
            StatModifierHandler = GetComponent<StatModifierHandler>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            DefaultColor = SpriteRenderer.color;
            LevelCompositeRoot.Instance.LevelInfo.RegisterEntity(this);
        }
    }
}
