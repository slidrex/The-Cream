using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Player
{
    internal class PlayerStats : EntityStats, IDamageable, IMoveable, IHaveTargetRadius
    {
        public int MaxHealth { get; set; }

        public int CurrentHealth { get; set; }

        public bool IsInvulnerable => false;

        public float MaxSpeed { get; set; }

        public float CurrentSpeed { get; }
        public float TargetRadius { get; set; }

        public PlayerStats(int maxHealth, float maxSpeed, float attackDistance) 
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            MaxSpeed = maxSpeed;
            CurrentSpeed = maxSpeed;
            TargetRadius = attackDistance;
        }
    }
}
