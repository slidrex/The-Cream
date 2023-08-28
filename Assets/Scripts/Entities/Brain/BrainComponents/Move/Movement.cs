using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
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
        private List<float> _disables = new();
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _rb.freezeRotation = true;
        }

        public void EnableMovement(bool enable, float disableTime = -10)
        {
            IsMovementDisabled = !enable;

            if (enable == false)
            {
                Stop();
                if(disableTime != -10)
                    _disables.Add(disableTime);
            }
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

            MoveVector = vector;
            if (vector == Vector2.zero)
            {
                IsMoving = false;
            }
            else IsMoving = true;

            float resultVector = Mathf.Abs(MoveVector.x) + Mathf.Abs(MoveVector.y);
            _animator.SetInteger(MOVE_X_TRIGGER, (int)resultVector);
        }
        private void UpdateMovementSpeed()
        {
            _rb.velocity = MoveVector * _stats.GetValue();
        }
        public void Stop()
        {
            if(IsMoving)
                SetMoveDirection(Vector2.zero);
        }
        private void Update()
        {
            UpdateMovementSpeed();
            UpdateDisableStatus();
        }
        private void UpdateDisableStatus()
        {
            for(int i = 0; i < _disables.Count; i++)
            {
                var dis = _disables[i];
                if(dis != -10)
                {
                    if (dis < 0)
                    {
                        _disables.RemoveAt(i);
                    }
                    else _disables[i] -= Time.deltaTime;
                }
                if (_disables.Count == 0) IsMovementDisabled = false;
            }
        }
        public void OnLevelRun(bool run)
        {
            if (!run) Stop();
        }
    }
}
