using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Templates;
using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Mobs.Mobs.Sunstriker
{
    internal class Sunstriker : ChaseMob, IHealthChangedHandler
    {
        public override int OnDieExp => 12;

        public override byte SpaceRequired => 5;

        public override EntityTypeBase TargetType => new EntityType<PlayerTag>(PlayerTag.PLAYER);
        public override AttributeHolder Stats => new AttributeHolder(new MaxHealthStat(20), new DamageStat(1));

        Action<int, int, Entity> IHealthChangedHandler.OnHealthChanged { get; set; }
    }
}
