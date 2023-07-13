using Assets.Scripts.Entities.Mobs;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers
{

    internal abstract class EntityStatModifier : StatDecorator
    {
        protected EntityStats StatsProvider;
        public EntityStatModifier(EntityStats statProvider)
        {
            StatsProvider = statProvider;
        }
        public abstract bool ModifyStats();
    }
}
