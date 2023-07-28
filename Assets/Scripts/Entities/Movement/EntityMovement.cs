using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Movement.Interfaces;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Navigation.Navigator;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class EntityMovement : MonoBehaviour, ILevelRunHandler
    {
        private SpeedStat _stats;
        private Rigidbody2D _rb;
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

            if (LevelCompositeRoot.Instance.Runner.IsLevelRunning == false)
            {
                return;
            }
            _rb.velocity = vector * _stats.GetValue();
            if (vector == Vector2.zero) IsMoving = false;
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
