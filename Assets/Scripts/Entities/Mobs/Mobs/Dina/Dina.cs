using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Templates;
using System;

namespace Assets.Scripts.Entities.Mobs.Mobs.Dina
{
    internal class Dina : ChaseMob, IHealthChangedHandler
    {
        public override int OnDieExp => 8;

        public override byte SpaceRequired => 5;

        public override EntityTypeBase TargetType => new EntityType<PlayerTag>().Any();
        public override AttributeHolder Stats => new AttributeHolder(new SpeedStat(3), new MaxHealthStat(12), new DamageStat(4), new AttackSpeedStat(1));

        public Action<int, int, Entity> OnHealthChanged { get; set; }
    }
}

