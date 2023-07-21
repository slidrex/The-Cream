using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Interfaces.Stats
{
    internal interface IDamageable 
    {
        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }
        bool IsInvulnerable { get; }
        void Heal(int heal);
        void Damage(int damage, Entity dealer);
        void OnDie();
    }
}
