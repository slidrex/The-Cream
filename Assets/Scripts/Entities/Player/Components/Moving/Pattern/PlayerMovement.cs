using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Player.Components.Attacking;
using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Entities.Structures.Portal;
using Assets.Scripts.Entities.Util.UIPlayer;
using Pathfinding;
using System;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets.Scripts.Entities.Player.Moving
{
	[RequireComponent(typeof(Movement), typeof(PlayerPointMovement), typeof(Facing))]
	[RequireComponent (typeof(Seeker), typeof(PlayerTargetMovement), typeof(MeleeAttack))]
	internal class PlayerMovement : EntityBrain<Player>
	{
		public Action<TargetType> OnMoveTargetSelect;
		[SerializeField] private float _safeDistance;
		[SerializeField] private LayerMask _tilemapLayer;
		private Animator _animator;
		private PlayerPointMovement _pointMovement;
		private Seeker _seeker;
		private Path _path;
		private int currentWaypoint;
		private Movement _movement;
		private bool _isMoving;
		private PlayerTargetMovement _targetMovement;
		private MeleeAttack _attack;
		private Facing _facing;
		private Entity _lastSelectedEntity;
        private const string MOVE_X_TRIGGER = "moveX";
        public enum TargetType
		{
			POINT,
			ENEMY,
			OBSTACLE
		}
		private TargetType _targetType;

		private void Start()
		{
			_animator = GetComponent<Animator>();
			_targetMovement = GetComponent<PlayerTargetMovement>();
			_facing = GetComponent<Facing>();
			_seeker = GetComponent<Seeker>();
			_pointMovement = GetComponent<PlayerPointMovement>();
			_movement = GetComponent<Movement>();
			_attack = GetComponent<MeleeAttack>();
		}
		private void OnEnable()
		{

			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += (GameMode mode) => Stop();
		}
		private void OnDisable()
		{
			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= (GameMode mode) => Stop();
		}
		public override void OnReset()
		{
			Stop();
			Debug.Log("On reset");
			InterruptAnimation();
        }
        protected override void OnUpdate()
        {
			if(Editor.Editor.Instance.GameModeIs(Editor.GameMode.RUNTIME) || Editor.Editor.Instance.GameModeIs(Editor.GameMode.NONE))
			{
				UpdatePosition();
			}
        }
		private void UpdatePosition()
		{

            if (_isMoving) UpdatePlayerPosition();
            if (Util.Config.Input.InputManager.IsActionKeyPressed(true, out Vector2 worldMousePos))
			{
				RaycastHit2D hit = Physics2D.Raycast(worldMousePos, Vector2.zero);
				if (hit.collider == null)
				{
					return;
				}

				if ((_tilemapLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
				{
					SetPoint();
				}
				else if(hit.collider.gameObject.TryGetComponent<BaseEntity>(out var entity))
				{
					if(entity is IPlayerSelectTrigger trigger) trigger.OnSelect();
					if(entity is Entity e)
						SetTarget(e);
				}
				OnMoveTargetSelect?.Invoke(_targetType);
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
			_path = null;
		}
		private void SetPoint()
		{
			SelectEntity(null);
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
			SelectEntity(entity);
			SetNewPath();
		}
		private void Stop()
		{
			_isMoving = false;
			SelectEntity(null);

            _movement?.Stop();
            _animator?.SetInteger(MOVE_X_TRIGGER, 0);
        }
		private void InterruptAnimation()
		{
            _facing?.SetSightDirection(Facing.SightDirection.RIGHT);
        }
		private void SetNewPath()
		{
			currentWaypoint = 0;
			_isMoving = true;
		}
		private void UpdatePlayerPosition()
		{
			
            Vector2 moveVector = Vector2.zero;
			if(_targetMovement.Target == null && _targetType == TargetType.ENEMY)
			{
				FindNewEnemy();
				return;
			}

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
			else if (_targetType == TargetType.ENEMY)
			{
				_targetMovement.GetMoveVectorEnemy(out moveVector, currentWaypoint, out currentWaypoint, _path, out bool insideRedZone);
				if (insideRedZone) _attack.OnTargetInsideZone(_targetMovement.Target);
				else _attack.OnTargetLeftZone();

			}
			else if (_targetType == TargetType.OBSTACLE) _targetMovement.GetMoveVectorTarget(out moveVector, currentWaypoint, out currentWaypoint, _path);

			_attack.UpdateAttackTarget(_targetType);
			if (moveVector != Vector2.zero)
			{
				_movement.SetMoveDirection(moveVector);
				_facing.SetSightDirection(moveVector.x < 0 ? Facing.SightDirection.LEFT : Facing.SightDirection.RIGHT);
			}
			else _movement.Stop();


            if (_animator != null)
			{
                float resultVector = Mathf.Abs(moveVector.x) + Mathf.Abs(moveVector.y);
                _animator.SetInteger(MOVE_X_TRIGGER, Mathf.RoundToInt(resultVector));
            }
		}
		private void FindNewEnemy()
		{
			Stop();
			var newEnemy = NavigationUtil.GetClosestEntityOfType(Entity.TargetType, Entity);
			if (newEnemy != null)
			{
				_targetMovement.SetEnemy(newEnemy, _safeDistance);
				SelectEntity(newEnemy);
				SetNewPath();
			}
		}
		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, _safeDistance);
		}
	}
}
