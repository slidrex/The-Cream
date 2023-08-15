using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.LightEater
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/LightEater/Requiem")]
    internal class Requiem : PlayerUndirectSkill<Characters.LightEater>
    {
        private Implementations.LightEater.Requiem _abil;
        public override void OnStart(Player player)
        {
            _abil = player.GetComponent<Implementations.LightEater.Requiem>();
        }
        protected override void OnActivate(Characters.LightEater player)
        {
            _abil.StartAbility();
        }
    }
}
