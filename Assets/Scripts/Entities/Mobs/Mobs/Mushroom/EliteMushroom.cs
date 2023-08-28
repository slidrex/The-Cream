using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Templates;
using System;

namespace Assets.Scripts.Entities.Player.Characters
{
    internal class EliteMushroom : ChaseMob, IHealthChangedHandler
    {
        public override int OnDieExp => 16;

        public override byte SpaceRequired => 1;

        public override EntityTypeBase TargetType => new EntityType<PlayerTag>(PlayerTag.PLAYER);
        public override AttributeHolder Stats => new AttributeHolder(new SpeedStat(2), new MaxHealthStat(30), new AttackSpeedStat(2));

        public Action<int, int, Entity> OnHealthChanged { get; set; }
    }
}
