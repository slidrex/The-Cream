﻿using Assets.Scripts.Entities.Mobs;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers
{

    internal abstract class EntityStatModifier : StatDecorator
    {
        protected Entity StatsProvider;
        public EntityStatModifier(Entity statProvider)
        {
            StatsProvider = statProvider;
        }
        public abstract bool OnEffectStart();
    }
}
