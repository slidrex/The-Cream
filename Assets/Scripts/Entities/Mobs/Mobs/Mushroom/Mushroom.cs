using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Templates;
using System;

namespace Assets.Scripts.Entities.Player.Characters
{
    internal class Mushroom : ChaseMob, IHealthChangedHandler
    {
        public override int OnDieExp => 7;

        public override byte SpaceRequired => 4;

        public override EntityTypeBase TargetType => new EntityType<PlayerTag>(PlayerTag.PLAYER);
        public override AttributeHolder Stats => new AttributeHolder(new DamageStat(10), new SpeedStat(2), new MaxHealthStat(15), new AttackSpeedStat(2));

        public Action<int, int, Entity> OnHealthChanged { get; set; }
    }
}
