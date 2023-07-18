using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Skills;

namespace Assets.Scripts.Databases.dto.Units
{
    internal class PlayerSkillModel : ScriptableObject
    {
        public PlayerSkill Skill;
        public Model GetModel()
        {
            return new Model(Skill);
        }
        public struct Model
        {
            public Sprite Icon;
            public bool IsPassive;
            public PlayerSkill Skill;

            public Model(PlayerSkill skill) : this()
            {
                Skill = skill;
                IsPassive = skill is PlayerPassiveSkill;
                Icon = skill.Icon;
            }
        }
    }
}
