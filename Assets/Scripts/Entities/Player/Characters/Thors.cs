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
        public override AttributeHolder Stats { get; } = new AttributeHolder(new MaxHealthStat(50), new SpeedStat(5), new DamageStat(8), new AttackSpeedStat(1));
    }
}
