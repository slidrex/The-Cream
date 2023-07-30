using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Util.UIPlayer;
using Pathfinding;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets.Scripts.Entities.Player.Moving
{
	[RequireComponent(typeof(Movement), typeof(PlayerPointMovement), typeof(Facing))]
	[RequireComponent (typeof(Seeker), typeof(PlayerTargetMovement))]
	internal class PlayerMovement : EntityBrain<Player>
	{
		[SerializeField] private float _safeDistance;
		[SerializeField] private LayerMask _tilemapLayer;
		private PlayerPointMovement _pointMovement;
		private Seeker _seeker;
		private Path _path;
		private int currentWaypoint;
		private Movement _movement;
		private bool _isMoving;
		private PlayerTargetMovement _targetMovement;
		private Facing _facing;
		private Entity _lastSelectedEntity;
		public enum TargetType
		{
			POINT,
			ENEMY,
			OBSTACLE
		}
		private TargetType _targetType;

		private void Start()
		{
			_targetMovement = GetComponent<PlayerTargetMovement>();
			_facing = GetComponent<Facing>();
			_seeker = GetComponent<Seeker>();
			_pointMovement = GetComponent<PlayerPointMovement>();
			_movement = GetComponent<Movement>();
		}
		public override void OnReset()
		{
			Stop();
		}
		protected override void RuntimeUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider == null)
				{
					return;
				}

				if ((_tilemapLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
				{
					SetPoint();
				}
				else if(hit.collider.gameObject.TryGetComponent<Entity>(out var entity))
				{
					SetTarget(entity);
				}
			}
		}
		private void SelectEntity(Entity entity)
		{
			if(_lastSelectedEntity != null)
				_lastSelectedEntity.SpriteRenderer.color = _lastSelectedEntity.DefaultColor;
			if (entity != null)
			{
				_lastSelectedEntity = entity;
				_lastSelectedEntity.SpriteRenderer.color = Color.magenta;
			}
		}
		private void SetPoint()
		{
			SelectEntity(null);
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_path = _pointMovement.SetPoint(_seeker, pos);
			Editor.Editor.Instance.PlayerMarks.SetMovePoint(pos);
			SetNewPath();
			_targetType = TargetType.POINT;
		}
		private void SetTarget(Entity entity)
		{
			if (entity.GetInstanceID() == Entity.GetInstanceID())
			{
				SelectEntity(null);
				Stop();
				return;
			}
			else if (Entity.TargetType.MatchesTag(entity))
			{
				_targetMovement.SetEnemy(entity, _safeDistance);
				_targetType = TargetType.ENEMY;
			}
			else
			{
				_targetMovement.SetTarget(entity.transform);
				_targetType = TargetType.OBSTACLE;
			}
			_path = null;
			SelectEntity(entity);
			SetNewPath();
		}
		private void Stop()
		{
			_isMoving = false;
			_path = null;
			SelectEntity(null);
			_movement.Stop();
		}
		protected override void RuntimeFixedUpdate()
		{
			if (_isMoving) UpdatePlayerPosition();
		}
		private void SetNewPath()
		{
			currentWaypoint = 0;
			_isMoving = true;
		}
		private void UpdatePlayerPosition()
		{
			Vector2 moveVector = Vector2.zero;


			if (_targetType != TargetType.POINT && (_targetMovement.IsWithinSafeDistance() == false || _targetType == TargetType.OBSTACLE))
			{
				var tempPath = _targetMovement.GetPath(_seeker, _path == null);

				if(tempPath != null)
				{
					_path = tempPath;
					SetNewPath();
				}
			}

			if (_targetType == TargetType.POINT) _isMoving = _pointMovement.TryGetMoveVector(out moveVector, currentWaypoint, out currentWaypoint, _path);
			else if(_targetType == TargetType.ENEMY) _targetMovement.GetMoveVectorEnemy(out moveVector, currentWaypoint, out currentWaypoint, _path, out bool insideRedZone);
			else if(_targetType == TargetType.OBSTACLE) _targetMovement.GetMoveVectorTarget(out moveVector, currentWaypoint, out currentWaypoint, _path);


			if (moveVector != Vector2.zero)
			{
				_movement.SetMoveDirection(moveVector);
				_facing.SetSightDirection(moveVector.x < 0 ? Facing.SightDirection.LEFT : Facing.SightDirection.RIGHT);
			}
			else _movement.Stop();
		}
		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, _safeDistance);
		}
	}
}
