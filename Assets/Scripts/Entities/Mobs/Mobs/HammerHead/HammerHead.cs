using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Templates;
using System;

namespace Assets.Scripts.Entities.Mobs.Mobs.HammerHead
{
    internal class HammerHead : ChaseMob, IHealthChangedHandler
    {
        public override int OnDieExp => 6;

        public override byte SpaceRequired => 4;

        public override EntityTypeBase TargetType => new EntityType<PlayerTag>(PlayerTag.PLAYER);

        public override AttributeHolder Stats => new AttributeHolder(new SpeedStat(2), new MaxHealthStat(10), new DamageStat(3), new AttackSpeedStat(1));

        public Action<int, int, Entity> OnHealthChanged { get; set; }
    }
}
