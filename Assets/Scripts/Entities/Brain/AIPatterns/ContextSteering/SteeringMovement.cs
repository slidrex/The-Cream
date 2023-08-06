using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering
{
    [RequireComponent(typeof(EnvironmentData))]
    internal class SteeringMovement : MonoBehaviour
    {
        private EnvironmentData _aiData;
		[SerializeField] private bool _showGizmos;
        [Header("Obstacle Movement")]
        [SerializeField] private float _criticalObstacleDistance = 0.5f;
		[Header("Chase Movement")]
		public float SafeDistance;
        public bool ConsiderSafeDistance { get; set; }
        private void Awake()
        {
            _aiData = GetComponent<EnvironmentData>();
        }
        public Vector2 GetDirectionToMove()
        {
            float[] danger = new float[8];
            float[] interest = new float[8];


            (danger, interest) = GetObstacleSteering(danger, interest);
            (danger, interest) = GetChaseSteering(danger, interest);

            //subtract danger values from interest array
            for (int i = 0; i < 8; i++)
            {
                interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
            }

            //get the average direction
            Vector2 outputDirection = Vector2.zero;
            for (int i = 0; i < 8; i++)
            {
                outputDirection += Directions.eightDirections[i] * interest[i];
            }

            outputDirection.Normalize();

            return outputDirection;
        }
#region Patterns
		private (float[] danger, float[] interest) GetObstacleSteering(float[] danger, float[] interest)
		{
			foreach (Collider2D obstacleCollider in _aiData.Obstacles)
			{
				if (obstacleCollider == null) return (danger, interest);
				Vector2 directionToObstacle
					= obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
				float distanceToObstacle = directionToObstacle.magnitude;

				//calculate weight based on the distance Enemy<--->Obstacle
				float weight
					= distanceToObstacle <= _criticalObstacleDistance
					? 1
					: 1 - distanceToObstacle/8;

				Vector2 directionToObstacleNormalized =  directionToObstacle.normalized;

				//Add obstacle parameters to the danger array
				for (int i = 0; i < Directions.eightDirections.Count; i++)
				{
					float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);

					float valueToPutIn = result * weight;

					//override value only if it is higher than the current one stored in the danger array
					if (valueToPutIn > danger[i])
					{
						danger[i] = valueToPutIn;
					}
				}
			}

			return (danger, interest);
		}
		private (float[] danger, float[] interest) GetChaseSteering(float[] danger, float[] interest)
		{
			if (_aiData.CurrentTarget == null) return (danger, interest);
			Vector2 targetPositionCached = _aiData.TargetCollider.ClosestPoint(transform.position);

			if (ConsiderSafeDistance && Vector2.SqrMagnitude(targetPositionCached - (Vector2)transform.position) < SafeDistance * SafeDistance)
			{
				_aiData.IsReachedTarget = true;
				return (danger, interest);
			}
			else _aiData.IsReachedTarget = false;

			Vector2 directionToTarget = _aiData.CurrentTarget.position - transform.position;
			for (int i = 0; i < interest.Length; i++)
			{
				float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);


				if (result > 0)
				{
					float valueToPutIn = result;
					if (valueToPutIn > interest[i])
					{
						interest[i] = valueToPutIn;
					}

				}
			}
			return (danger, interest);
		}
		#endregion
		private void OnDrawGizmos()
		{
			if (_showGizmos)
			{
				Gizmos.DrawWireSphere(transform.position, _criticalObstacleDistance);
				Gizmos.DrawWireSphere(transform.position, SafeDistance);
			}
		}
	}
	public static class Directions
    {
        public static List<Vector2> eightDirections = new (){
            new Vector2(0,1).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized
        };
    }
}
