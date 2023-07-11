using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Movement.Mob.Dummy
{
    internal class DummyStats : EntityStats, IMoveable, IDamageable
    {
        public int MaxSpeed { get; set; }

        public int CurrentSpeed { get; set; }
        public int MaxHealth { get; set; }

        public int CurrentHealth { get; set; }

        public bool IsInvulnerable { get; set; }

        public DummyStats(int maxHealth, int maxSpeed)
        {
            MaxHealth = maxHealth;
            MaxSpeed = maxSpeed;
            CurrentSpeed = maxSpeed;
            CurrentHealth = maxHealth;
        }
    }
}
