using Assets.Scripts.Entities.Player.Skills.Implementations.LightEater;
using Assets.Scripts.Entities.Stats.Interfaces.Attack;
using Assets.Scripts.Entities.Stats.Interfaces.Detect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Player.Characters
{
    [UnityEngine.RequireComponent(typeof(ShadowWalk))]
    internal class LightEater : Player, IUndetectable, IAttackMutable
    {
        public bool IsUndetectable { get; set; }
        public bool MutedAttack { get; set; }
    }
}
