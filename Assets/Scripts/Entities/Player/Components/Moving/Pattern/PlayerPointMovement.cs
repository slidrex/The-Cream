using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Moving
{
	internal class PlayerPointMovement : PlayerMovementPattern
	{
		public Path SetPoint(Seeker seeker, Vector2 point)
		{
			return seeker.StartPath(transform.position, new Vector3(point.x, point.y, Camera.main.transform.position.z));
		}
		public bool TryGetMoveVector(out Vector2 moveVector, int currentWaypoint, out int newWaypoint, Path path)
		{
			moveVector = Vector2.zero;
			newWaypoint = currentWaypoint;

			if (TryGetNextWaypoint(currentWaypoint, path, out int newWay))
			{
				moveVector = (path.vectorPath[newWay] - transform.position).normalized;
				newWaypoint = newWay;
				return true;
			}
			else return false;
		}
	}
}
