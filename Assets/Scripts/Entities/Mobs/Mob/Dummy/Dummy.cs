using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.Templates;
using Assets.Scripts.Entities.Stats.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Movement.Mob.Dummy
{
    internal class Dummy : Mobs.Mob, IBaseMobStatsProvider
    {
        public override byte SpaceRequired => 8;

        public override EntityTypeBase ThisType => new EntityType<MobTag>(MobTag.AGGRESSIVE);

        public int MaxHealth { get; set; } = 30;
        public int CurrentHealth { get; set; }

        public bool IsInvulnerable { get; set; }

        public float TargetRadius { get; set; } = 3.0f;

        public float CurrentSpeed { get; set; } = 1.5f;
        public Action<int> OnDamage { get; set; }
        public Action<int> OnHeal { get; set; }
        public float AttackSpeed { get; set; } = 1;
        int ICanDamage.AttackDamage { get; set; } = 8;

        public void Damage(int damage)
        {
            EntityHealthStrategy.Damage(this, damage);
        }

        public void Heal(int heal)
        {
            EntityHealthStrategy.Heal(this, heal);
        }

        public void OnDie()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            EntityHealthStrategy.ResetHealth(this);
        }
    }
}
