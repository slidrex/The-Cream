using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Brain
{
    internal class EntityBrain : MonoBehaviour
    {
        public enum SightDirection
        {
            RIGHT,
            LEFT
        }
        protected Entity Entity { get; private set; }
        public const int RIGHT = 1;
        public const int LEFT = -1;
        public int CurrentSight { get; private set; }
        public void SetSightDirection(SightDirection direction)
        {
            if (direction == SightDirection.RIGHT) transform.eulerAngles = Vector3.zero;
            else transform.eulerAngles = Vector3.up * 180;
            CurrentSight = direction == SightDirection.RIGHT ? RIGHT : LEFT;
        }
        protected void StalkTarget(Transform target)
        {
            if(target != null)
            SetSightDirection(target.position.x < transform.position.x ? SightDirection.LEFT : SightDirection.RIGHT);
        }
        private void Awake()
        {
            Entity = GetComponent<Entity>();
        }
        private void Update()
        {
            if (LevelCompositeRoot.Instance.Runner.IsLevelRunning)
            {
                RuntimeUpdate();
            }
        }
        protected virtual void RuntimeUpdate()
        {

        }
    }
}
