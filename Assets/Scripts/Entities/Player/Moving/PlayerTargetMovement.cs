using Pathfinding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Moving
{
	internal class PlayerTargetMovement : PlayerMovementPattern
	{
		private Transform _target;
		private bool _isEnemy;
		private float _safeDistance;
		public void SetTarget(Transform target)
		{
			_isEnemy = false;
			_target = target;
		}
		public void SetEnemy(Entity entity, float safeDistance)
		{
			_isEnemy = true;
			_safeDistance = safeDistance;
			_target = entity.transform;
		}
		public Path GetPath(Seeker seeker)
		{
			if (_target.hasChanged) return null;
			return seeker.StartPath(transform.position, new Vector3(_target.position.x, _target.position.y, Camera.main.transform.position.z));

		}
		public void GetMoveVector(out Vector2 moveVector, int currentWaypoint, out int newWaypoint, Path path, out bool saved)
		{
			moveVector = Vector2.zero;
			newWaypoint = currentWaypoint;

			saved = false;
			if(_isEnemy && IsWithinSafeDistance())
			{
				saved = true;
				return;
			}
			if (TryGetNextWaypoint(currentWaypoint, path, out int newWay))
			{
				moveVector = (path.vectorPath[newWay] - transform.position).normalized;
				newWaypoint = newWay;
			}
		}
		public bool IsWithinSafeDistance()
		{
			return Vector2.SqrMagnitude(_target.transform.position - transform.position) <= _safeDistance * _safeDistance;
		}
	}
}
