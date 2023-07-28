using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.Strategies;
using Assets.Scripts.Entities.Strategies;

namespace Assets.Scripts.Entities
{
    internal abstract class EditorConstructEntity : Entity, IPlaceable, IEditorSpaceRequired, IResettable
    {
        public abstract byte SpaceRequired { get; }
        public override AttributeHolder Stats => new(new MaxHealthStat(100));

        public virtual void OnContruct() { }

        public virtual void OnDeconstruct() { }
        protected virtual void OnEntityReset() { }
        public virtual void OnReset()
        {
            EntityHealthStrategy.ResetHealth(this);
            OnEntityReset();
        }
    }
}
