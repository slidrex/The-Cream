using Assets.Scripts.Entities.Mobs;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers
{
    internal struct Modifier
    {
        public float RemainTime { get; private set; }
        public EntityStatModifier Mod { get; private set; }
        internal Modifier(EntityStatModifier mod)
        {
            Mod = mod;
            RemainTime = mod.Duration;
        }
    }
    internal abstract class EntityStatModifier : StatDecorator
    {
        internal abstract float Duration { get; }
        protected EntityStats StatsProvider;
        public EntityStatModifier(EntityStats statProvider)
        {
            StatsProvider = statProvider;
        }
        public abstract EntityStats GetStats();
    }
}
