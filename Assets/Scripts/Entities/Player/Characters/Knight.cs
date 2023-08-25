using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Player.Characters
{
    internal sealed class Knight : Player
    {
        public override AttributeHolder Stats { get; } = new AttributeHolder(new MaxHealthStat(100), new SpeedStat(4), new DamageStat(5), new AttackSpeedStat(1));
        public override void OnLevelUp()
        {
            Stats.Modify<DamageStat>(new Entities.Stats.StatAttributes.AttributeMask() { BaseValue = 0.2f });
            print("i have a bad news. i've level uped");
        }
    }
}
