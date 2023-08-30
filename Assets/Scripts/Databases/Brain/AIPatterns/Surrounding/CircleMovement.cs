using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.Entities.AI.Surrounding
{
    internal class CircleMovement : MonoBehaviour
    {
        private Vector2 _assignedPosition;
        private float _afkTime;
		[SerializeField] private float MIN_afkTime = 0.3f;
		[SerializeField] private float MAX_afkTime = 4.6f;
        private float _afkTimerRequired;
		private bool _reorederAfk;
        public Vector2 GetMoveDirection(Transform target, float minSafe, float maxSafe)
        {

            if((_assignedPosition - (Vector2)transform.position).sqrMagnitude <= 0.5f || _reorederAfk)
            {
                _reorederAfk = false;
                UpdateAfkTimer();

				_assignedPosition = (Vector2)target.position + new Vector2(Mathf.Sin(Random.Range(0, 360)) * Random.Range(minSafe, maxSafe), Mathf.Sin(Random.Range(0, 360)) * Random.Range(minSafe, maxSafe));
            }

            return (_assignedPosition - (Vector2)transform.position).normalized;

        }
        private void Update()
        {
            if(_afkTime >= _afkTimerRequired)
            {
                UpdateAfkTimer();
				_reorederAfk = true;
			}
            else _afkTime += Time.deltaTime;
        }
        private void UpdateAfkTimer()
        {
            _afkTime = 0;
			_afkTimerRequired = Random.Range(MIN_afkTime, MAX_afkTime);
        }
    }
}
