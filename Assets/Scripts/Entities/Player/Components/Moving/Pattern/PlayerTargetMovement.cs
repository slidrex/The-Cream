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
		public Entity Target { get; private set; }
		private float _safeDistance;
		public void SetTarget(Transform target)
		{
			_target = target;
		}
		public void SetEnemy(Entity entity, float safeDistance)
		{
			Target = entity;
			_safeDistance = safeDistance;
			_target = entity.transform;
		}
		public Path GetPath(Seeker seeker, bool isNull)
		{
			if (_target != null && !_target.transform.hasChanged && isNull == false) return null;
			_target.transform.hasChanged = false;
			return seeker.StartPath(transform.position, new Vector3(_target.position.x, _target.position.y, _target.position.z));

		}
		public void GetMoveVectorTarget(out Vector2 moveVector, int currentWaypoint, out int newWaypoint, Path path)
		{
			moveVector = Vector2.zero;
			newWaypoint = currentWaypoint;


			if (TryGetNextWaypoint(currentWaypoint, path, out int newWay))
			{
				moveVector = (path.vectorPath[newWay] - transform.position).normalized;
				newWaypoint = newWay;
			}
		}
		public void GetMoveVectorEnemy(out Vector2 moveVector, int currentWaypoint, out int newWaypoint, Path path, out bool reachedTreshold)
		{
			moveVector = Vector2.zero;
			newWaypoint = currentWaypoint;
			reachedTreshold = false;

			if((_target.transform.position - transform.position).sqrMagnitude <= _safeDistance * _safeDistance)
			{
				reachedTreshold = true;
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
