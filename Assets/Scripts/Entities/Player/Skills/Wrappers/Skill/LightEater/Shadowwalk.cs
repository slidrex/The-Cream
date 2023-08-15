using Assets.Scripts.Entities.Player.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.LightEater
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/LightEater/Shadow walk")]
    internal class Shadowwalk : PlayerUndirectSkill<Characters.LightEater>
    {
        [UnityEngine.SerializeField] private float _duration;
        private Implementations.LightEater.ShadowWalk _abil;
        public override void OnStart(Player player)
        {
            _abil = player.GetComponent<Implementations.LightEater.ShadowWalk>();
        }
        protected override void OnActivate(Characters.LightEater player)
        {
            _abil.StartWalk(_duration, player);
        }
    }
}
