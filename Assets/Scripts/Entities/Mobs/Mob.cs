using Assets.Scripts.Entities.EntityExperienceLevel;

namespace Assets.Scripts.Entities.Mobs
{
    internal abstract class Mob : EditorConstructEntity, IExperienceGainer
    {
        public virtual int OnDieExp { get; }
    }
}
