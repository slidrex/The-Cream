using Assets.Scripts.Entities.Player.Skills.Wrappers;
using Assets.Scripts.Entities.Player.SpecialAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Player.Skills.Skill.Thors
{
	[CreateAssetMenu(menuName = "Cream/Database/Character/Skill/Thors/Thors Shield")]
	internal class ThorsShield : PlayerUndirectSkill
    {
		[SerializeField] private int _maxDamageAbsorbtion;
		[SerializeField, Range(0, 1.0f)] private float _additionalAttackSpeedWhileShieldActivated;
		private Implementations.Thors.ThorsShield _shield;
		public override void OnStart(Player player)
		{
			_shield = player.GetComponent<Implementations.Thors.ThorsShield>();
		}
		protected override void OnActivate(Player player)
		{
			_shield.Activate(_maxDamageAbsorbtion, _additionalAttackSpeedWhileShieldActivated);
		}
	}
}
