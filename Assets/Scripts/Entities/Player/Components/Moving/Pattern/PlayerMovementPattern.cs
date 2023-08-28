using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Pathfinding.Funnel;

namespace Assets.Scripts.Entities.Player.Moving
{
	internal class PlayerMovementPattern : MonoBehaviour
	{
		protected bool TryGetNextWaypoint(int currentWaypoint, Path path, out int newWaypoint)
		{
			newWaypoint = currentWaypoint;

			if (path == null || path.vectorPath.Count <= currentWaypoint)
			{
				return false;
			}
			bool isInCurrentPoint = Vector2.SqrMagnitude(path.vectorPath[currentWaypoint] - transform.position) <= 0.2f;
			if (isInCurrentPoint)
			{
				if (path.vectorPath.Count <= currentWaypoint + 1) return false;
				else newWaypoint++;
				return true;
			}
			return true;
		}
	}
}
