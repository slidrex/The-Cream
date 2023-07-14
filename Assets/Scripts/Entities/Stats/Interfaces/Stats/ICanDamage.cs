using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Interfaces.Stats
{
    internal interface ICanDamage
    {
        int AttackDamage { get; set; }
        float AttackSpeed { get; set; }
    }
}
