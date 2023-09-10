using Assets.Scripts.Entities.AI.ContextSteering;
using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Stats.Interfaces.Attack;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Entities.Player.Components.Attacking;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Components.Attacking
{
	internal sealed class MeleeAttack : PlayerAttack
	{
		protected override void OnPerformAttack(Transform target)
		{
			if (Data.GetTarget() != null && Data.GetTarget() is IDamageable d)
			{
				d.Damage(Damage, Entity);
				OnAttack();
			}
		}
	}
}
