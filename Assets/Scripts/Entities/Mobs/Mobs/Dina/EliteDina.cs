using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;

namespace Assets.Scripts.Entities.Mobs.Mobs.Dina
{
    internal class EliteDina : Dina, IHealthChangedHandler
    {
        public override int OnDieExp => 15;

        public override byte SpaceRequired => 1;

        public override EntityTypeBase TargetType => new EntityType<PlayerTag>().Any();
        public override AttributeHolder Stats => new AttributeHolder(new SpeedStat(3), new MaxHealthStat(35), new DamageStat(4), new AttackSpeedStat(1));
    }
}

