using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Stats.StatAttributes;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers
{

    public abstract class EntityStatModifier : StatDecorator
    {
        protected Entity StatsProvider;
        public EntityStatModifier(Entity statProvider)
        {
            StatsProvider = statProvider;
        }
        public abstract bool OnEffectStart();
    }
}
