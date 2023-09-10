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
        public override AttributeHolder Stats => new(new MaxHealthStat(100));

		public virtual void OnContruct()
        {
            StartCoroutine(Appear());
        }

        private IEnumerator Appear()
        {
            int multiplyValue = 10;
            float initialSize = transform.localScale.x;
            float increasingSize = 0; 
            while (increasingSize <= initialSize + 0.25f)
            {
                increasingSize += Time.deltaTime * multiplyValue;
                transform.localScale = new Vector2(increasingSize, increasingSize);
                yield return new WaitForEndOfFrame();
            }
            while (increasingSize >= initialSize)
            {
                increasingSize -= Time.deltaTime * multiplyValue;
                transform.localScale = new Vector2(increasingSize, increasingSize);
                yield return new WaitForEndOfFrame();
            }
            transform.localScale = new Vector2(initialSize, initialSize);
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
