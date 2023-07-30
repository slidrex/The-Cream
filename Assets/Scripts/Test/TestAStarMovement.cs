using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Test
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Seeker))]
	internal class TestAStarMovement : MonoBehaviour
	{
		private Rigidbody2D rb;
		private Seeker seeker;
		private Path path;
		private int currentWaypoint;
		private bool reachedEndOfPath;
		private float nextWaypointDistance = 0.4f;
		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			seeker = GetComponent<Seeker>();
		}
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				path = seeker.StartPath(transform.position, new Vector3(pos.x, pos.y, Camera.main.transform.position.z));
				currentWaypoint = 0;
				UpdateMoveVector();
				reachedEndOfPath = false;

			}
		}
		private void FixedUpdate()
		{
			AStarMovement();
			
		}
		private void AStarMovement()
		{
			if (path == null)
			{
				return;
			}
			if (currentWaypoint >= path.vectorPath.Count)
			{
				reachedEndOfPath = true;
				rb.velocity = Vector2.zero;
				return;
			}
			else
			{
				reachedEndOfPath = false;
			}
			Vector2 direction = path.vectorPath[currentWaypoint] - transform.position;
			float distance = Vector2.SqrMagnitude(path.vectorPath[currentWaypoint] - transform.position);
			if (distance < nextWaypointDistance * nextWaypointDistance)
			{
				currentWaypoint++;
				UpdateMoveVector();
			}
		}
		private void UpdateMoveVector()
		{
			if (currentWaypoint < path.vectorPath.Count)
				rb.velocity = (path.vectorPath[currentWaypoint] - transform.position) * 4;
		}
	}
}
