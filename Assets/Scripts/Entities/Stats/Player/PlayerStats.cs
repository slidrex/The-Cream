using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Player
{
    internal class PlayerStats : EntityStats, IDamageable, IMoveable
    {
        public int MaxHealth { get; set; }

        public int CurrentHealth { get; set; }

        public bool IsInvulnerable => false;

        public int MaxSpeed { get; set; }

        public int CurrentSpeed { get; }
        public PlayerStats(int maxHealth, int maxSpeed) 
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            MaxSpeed = maxSpeed;
        }
    }
}
