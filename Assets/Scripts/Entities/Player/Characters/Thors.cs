using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Player.Characters
{
	internal class Thors : Player
	{
        public override AttributeHolder Stats { get; } = new AttributeHolder(new MaxHealthStat(100), new SpeedStat(3), new DamageStat(5), new AttackSpeedStat(1));
    }
}
