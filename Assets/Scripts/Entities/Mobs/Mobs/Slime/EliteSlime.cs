using Assets.Scripts.Entities.Templates;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;

namespace Assets.Scripts.Entities.Mobs.Mobs.Slime
{
    internal class EliteSlime : ChaseMob, IHealthChangedHandler
    {
        public override int OnDieExp => 15;

        public override byte SpaceRequired => 1;

        public override EntityTypeBase TargetType => new EntityType<PlayerTag>(PlayerTag.PLAYER);
        public override AttributeHolder Stats => new AttributeHolder(new SpeedStat(1), new MaxHealthStat(20), new DamageStat(4), new AttackSpeedStat(2));

        public Action<int, int, Entity> OnHealthChanged { get; set; }
    }
}
