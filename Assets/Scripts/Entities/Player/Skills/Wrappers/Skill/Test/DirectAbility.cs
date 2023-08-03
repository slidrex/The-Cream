using Assets.Scripts.Entities.Mobs.Mobs.Slime;
using Assets.Scripts.Entities.Player.SpecialAbilities;
using Assets.Scripts.LevelEditor.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Test
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/Test/Direct ability")]
    internal class DirectAbility : PlayerActiveSkill
    {
        [SerializeField] private Slime _slime;
        protected override void OnActivate(Player player)
        {

        }
    }
}
