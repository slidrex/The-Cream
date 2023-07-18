using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.Templates;
using Assets.Scripts.Entities.Stats.Strategies;
using Assets.Scripts.Entities.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Movement.Mob.Dummy
{
    internal class Dummy : ChaseMob, IHealthChangedHandler
    {
        public override int MaxHealth { get; set; } = 40;
        public override int AttackDamage { get; set; } = 5;
        public override float AttackSpeed { get; set; } = 1.5f;
        public override float CurrentSpeed { get; set; } = 1.5f;
        public override byte SpaceRequired => 8;
        
        public override EntityTypeBase TargetType => new EntityType<PlayerTag>().Any();

        public Action<int> OnHealthChanged { get; set; }
    }
}
