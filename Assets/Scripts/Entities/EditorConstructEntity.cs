using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.Strategies;
using Assets.Scripts.Entities.Strategies;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    internal abstract class EditorConstructEntity : Entity, IPlaceable, IEditorSpaceRequired, IResettable
    {
        public abstract byte SpaceRequired { get; }
        private Rigidbody2D rb;
        public override AttributeHolder Stats => new(new MaxHealthStat(100));

		public virtual void OnContruct()
        {
            StartCoroutine(Appear());
        }
        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
        }

        private IEnumerator Appear()
        {
            int multiplyValue = 10;
            float initialSize = transform.localScale.x;
            float increasingSize = 0; 
            while (increasingSize <= initialSize)
            {
                increasingSize += Time.deltaTime * multiplyValue;
                transform.localScale = new Vector2(increasingSize, increasingSize);
                yield return new WaitForEndOfFrame();
            }
            transform.localScale = new Vector2(initialSize, initialSize);
        }
        private void OnEnable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += ChangeBodyType;
        }
        private void OnDisable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= ChangeBodyType;
        }

        private void ChangeBodyType(Editor.GameMode gameMode)
        {
            if (gameMode == GameMode.RUNTIME)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }

        public virtual void OnDeconstruct() { }
        protected virtual void OnEntityReset() { }
        public virtual void OnReset()
        {
            EntityHealthStrategy.ResetHealth(this);
            OnEntityReset();
        }
        
    }
}
