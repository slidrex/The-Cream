using Assets.Scripts.Entities.Placeable;
namespace Assets.Scripts.Entities
{
    internal abstract class EditorConstructEntity : Entity, IPlaceable, IEditorSpaceRequired
    {
        public abstract byte SpaceRequired { get; }
        public virtual void OnContruct()
        {

        }

        public virtual void OnDeconstruct()
        {

        }
    }
}
