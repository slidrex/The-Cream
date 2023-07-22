using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Movement.Interfaces;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Navigation.Navigator;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement
{
    [RequireComponent(typeof(Navigator))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class EntityMovement : MonoBehaviour, IMovement, ILevelRunHandler
    {
        private Navigator _navigator;
        private IMoveable _stats;
        private Rigidbody2D _rb;
        public Entity AttachedEntity { get; private set; }
        public bool IsMoving { get; private set; }
        private float _safeDistance;
        private void Start()
        {
            AttachedEntity = GetComponent<Entity>();
            _navigator = GetComponent<Navigator>();
            _rb = GetComponent<Rigidbody2D>();

            _stats = AttachedEntity as IMoveable;
            if (_stats == null) throw new Exception("Entity doesn't have IMoveable attribute");
        }
        public void SetMoveDirection(Vector2 vector)
        {
            _rb.velocity = vector * _stats.CurrentSpeed;
            if (vector == Vector2.zero) IsMoving = false;
        }
        public void Stop()
        {
            if (_rb.velocity != Vector2.zero) _rb.velocity = Vector2.zero;
            IsMoving = false;
        }
        public void MoveToTarget(bool stopIfSafeDistance = true)
        {
            var target = _navigator.GetTargetTransform();
            if (target != null)
            {
                Vector2 dist = target.position - transform.position;

                if(!stopIfSafeDistance || Vector2.SqrMagnitude(target.position - transform.position) >= _safeDistance * _safeDistance)
                    SetMoveDirection(dist.normalized);
                else Stop();
            }
            else Stop();
        }
        public void SetSafeDistance(float distance)
        {
            _safeDistance = distance;
        }
        public bool IsInsideSafeDistance(Transform target) => Vector2.SqrMagnitude(target.position - transform.position) <= _safeDistance * _safeDistance;

        public void OnLevelRun(bool run)
        {
            if (!run) Stop();
        }
    }
}
