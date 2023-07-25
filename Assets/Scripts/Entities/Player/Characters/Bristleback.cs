using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;

namespace Assets.Scripts.Entities.Player.Characters
{
    internal class Bristleback : Player
    {
        public override void OnLevelUp()
        {
            Stats.Modify<MaxHealthStat>(new Entities.Stats.StatAttributes.AttributeMask() { BaseValue = 10 });
            print("I have a good news!");
        }
    }
}
