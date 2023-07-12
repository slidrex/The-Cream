using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Navigation.Pulling
{
    internal class PullInfoHolder
    {
        private Queue<Transform> _pullQueue;
        private Transform _currentTarget;
        private Transform transform;
        public PullInfoHolder(Transform currentTransform)
        {
            transform = currentTransform;
            _pullQueue = new Queue<Transform>();
        }
        public bool TryGetPullTarget(out Transform target)
        {
            if(_currentTarget != null)
            {
                bool positionEquals = Mathf.Approximately(_currentTarget.position.x, transform.position.x) && Mathf.Approximately(_currentTarget.position.y, transform.position.y);
                if (positionEquals) _currentTarget = null;
            }
            if(_currentTarget == null)
            {
                _pullQueue.TryDequeue(out _currentTarget);
            }
            target = _currentTarget;
            return target != null;
        }
        public void AddPullTarget(Transform target)
        {
            _pullQueue.Enqueue(target);
        }
    }
}
