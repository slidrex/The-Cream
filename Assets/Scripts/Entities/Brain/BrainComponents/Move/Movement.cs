using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Level;
using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof (Animator))]
    internal sealed class Movement : MonoBehaviour, ILevelRunHandler
    {
        private SpeedStat _stats;
        private Rigidbody2D _rb;
        private Animator _animator;
        public bool IsMovementDisabled { get; private set; }
        public Vector2 MoveVector { get; private set; }
        public Entity AttachedEntity { get; private set; }
        public bool IsMoving { get; private set; }
        private const string MOVE_X_TRIGGER = "moveX";
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void EnableMovement(bool enable)
        {
            IsMovementDisabled = !enable;
            if (enable == false) Stop();
        }
        private void Start()
        {
            AttachedEntity = GetComponent<Entity>();

            _stats = AttachedEntity.Stats.GetAttribute<SpeedStat>();
            if (_stats == null) throw new Exception("Entity doesn't have IMoveable attribute");
        }
        public void SetMoveDirection(Vector2 vector)
        {
            if (IsMovementDisabled) vector = Vector2.zero;
			_rb.velocity = vector * _stats.GetValue();
            MoveVector = vector;
            if (vector == Vector2.zero)
            {
                IsMoving = false;
            }
            else IsMoving = true;

            float resultVector = Mathf.Abs(MoveVector.x) + Mathf.Abs(MoveVector.y);
            _animator.SetInteger(MOVE_X_TRIGGER, Mathf.RoundToInt(vector.x));
        }
        public void Stop()
        {
            if(IsMoving)
                SetMoveDirection(Vector2.zero);
        }
        public void OnLevelRun(bool run)
        {
            if (!run) Stop();
        }
    }
}
