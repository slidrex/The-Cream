using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Level;
using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class Movement : MonoBehaviour, ILevelRunHandler
    {
        private SpeedStat _stats;
        private Rigidbody2D _rb;
        public Vector2 MoveVector { get; private set; }
        public Entity AttachedEntity { get; private set; }
        public bool IsMoving { get; private set; }
        private void Start()
        {
            AttachedEntity = GetComponent<Entity>();
            _rb = GetComponent<Rigidbody2D>();

            _stats = AttachedEntity.Stats.GetAttribute<SpeedStat>();
            if (_stats == null) throw new Exception("Entity doesn't have IMoveable attribute");
        }
        public void SetMoveDirection(Vector2 vector)
        {

            if (LevelCompositeRoot.Instance.Runner.CurrentMode != Editor.GameMode.RUNTIME)
            {
                return;
            }

			_rb.velocity = vector * _stats.GetValue();
            MoveVector = vector;
            if (vector == Vector2.zero)
            {
                IsMoving = false;
            }
        }
        public void Stop()
        {
            if (_rb.velocity != Vector2.zero) _rb.velocity = Vector2.zero;
			IsMoving = false;
        }
        public void OnLevelRun(bool run)
        {
            if (!run) Stop();
        }
    }
}
