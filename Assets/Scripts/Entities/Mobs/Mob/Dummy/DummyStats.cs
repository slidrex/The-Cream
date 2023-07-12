using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Movement.Mob.Dummy
{
    internal class DummyStats : EntityStats, IMoveable, IDamageable, IHaveTargetRadius
    {
        public float MaxSpeed { get; set; }

        public float CurrentSpeed { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public bool IsInvulnerable { get; set; }
        public float TargetRadius { get; set; }

        public DummyStats(int maxHealth, int maxSpeed, float attackDistance)
        {
            MaxHealth = maxHealth;
            MaxSpeed = maxSpeed;
            CurrentSpeed = maxSpeed;
            CurrentHealth = maxHealth;
            TargetRadius = attackDistance;
        }
    }
}
