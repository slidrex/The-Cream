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
        private List<Transform> _pullQueue;
        private Transform _currentTarget;
        private Transform transform;
        public PullInfoHolder(Transform currentTransform)
        {
            transform = currentTransform;
            _pullQueue = new();
        }
        public bool TryGetPullTarget(out Transform target)
        {
            if(_currentTarget != null)
            {
                bool positionEquals = Vector2.SqrMagnitude(_currentTarget.transform.position - transform.position) <= 0.1f;
                if (positionEquals) _currentTarget = null;
            }
            if(_currentTarget == null && _pullQueue.Count > 0)
            {
                _currentTarget = _pullQueue[0];
                _pullQueue.RemoveAt(0);
            }
            target = _currentTarget;
            return target != null;
        }
        public void AddPullTarget(Transform target)
        {
            if(!_pullQueue.Contains(target))
                _pullQueue.Add(target);
        }
        public void TryRevoke(Transform target)
        {
            _pullQueue.Remove(target);
            if (target == _currentTarget) _currentTarget = null;
        }
    }
}
