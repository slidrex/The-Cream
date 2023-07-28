using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.Templates;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.Strategies;
using Assets.Scripts.Entities.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Movement.Mobs.Dummy
{
    internal class Dummy : ChaseMob, IHealthChangedHandler
    {
        public override byte SpaceRequired => 4;
        
        public override EntityTypeBase TargetType => new EntityType<PlayerTag>().Any();

        public Action<int, int, Entity> OnHealthChanged { get; set; }

        public override AttributeHolder Stats => new AttributeHolder(new SpeedStat(2), new MaxHealthStat(10), new DamageStat(1), new AttackSpeedStat(1));

        public override int OnDieExp => 5;
    }
}
