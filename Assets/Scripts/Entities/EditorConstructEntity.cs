using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Strategies;

namespace Assets.Scripts.Entities
{
    internal abstract class EditorConstructEntity : Entity, IPlaceable, IEditorSpaceRequired, IResettable
    {
        public abstract byte SpaceRequired { get; }

        public virtual void OnContruct()
        {

        }

        public virtual void OnDeconstruct()
        {

        }

        public void OnReset()
        {
            EntityBaseStrategy.OnReset(this);
            OnEntityReset();
        }
        protected virtual void OnEntityReset()
        {

        }
    }
}
