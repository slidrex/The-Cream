using Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills
{
    internal abstract class PlayerActiveSkill<T> : PlayerSkill, ICooldownResetter, ICooldownable, IActivatable where T : Player
    {
        [field: SerializeField] public int BaseManacost { get; set; }
        [field: SerializeField] public float BaseCooldown { get; set; }
        public bool DisableActivate;
        public float TimeSinceActivation { get; set; }
        public abstract bool TryActivate(SkillHolder holder, Player player, bool clickedByIcon);
        public override void Update()
        {
            if (TimeSinceActivation < BaseCooldown)
            {
                TimeSinceActivation += Time.deltaTime;
            }
        }
		public void ResetCooldown()
		{
            TimeSinceActivation = 0;
		}
	}
}
