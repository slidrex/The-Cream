using Assets.Scripts.Entities.Projectiles.Character.Bristleback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Skill.Bristleback
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/Thors/Spike spread")]
    internal class SpikeSpread : PlayerActiveSkill
    {
        [UnityEngine.SerializeField] private Bristlespike _spike;
        [UnityEngine.SerializeField] private byte _spikeCount;
        protected override void OnActivate(Player player)
        {
            for(int i = 0; i < 360; i += 360 / _spikeCount) 
            {
                var spike = Instantiate(_spike, player.transform.position, Quaternion.Euler(0, 0, i));

                spike.SetMoveDirection(spike.transform.up);
                spike.SetOwner(player);
            }
        }
    }
}