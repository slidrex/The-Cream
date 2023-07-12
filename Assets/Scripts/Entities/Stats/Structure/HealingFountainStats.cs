using Assets.Scripts.Entities.Stats.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Structure
{
    internal class HealingFountainStats : BaseStructureStats, ICanHeal
    {
        public float HealPercent { get; set; }
        public HealingFountainStats(float targetRadius, float healingPercent) : base(targetRadius)
        {
            HealPercent = healingPercent;
        }

    }
}
