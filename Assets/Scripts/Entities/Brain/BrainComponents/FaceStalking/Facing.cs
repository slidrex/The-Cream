using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.SightStalking
{
	internal sealed class Facing : MonoBehaviour
	{
		public const int RIGHT = 1;
		public const int LEFT = -1;
		public enum SightDirection
		{
			RIGHT,
			LEFT
		}
		public int CurrentSight { get; private set; }
		public void SetSightDirection(SightDirection direction)
		{
			if (direction == SightDirection.RIGHT) transform.eulerAngles = Vector3.zero;
			else transform.eulerAngles = Vector3.up * 180;
			CurrentSight = direction == SightDirection.RIGHT ? RIGHT : LEFT;
		}
		public void StalkTarget(Transform target)
		{
			if (target != null)
				SetSightDirection(target.position.x < transform.position.x ? SightDirection.LEFT : SightDirection.RIGHT);
		}
	}
}
