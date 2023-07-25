using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering.Behaviours
{
    internal class ObstacleBehavour : SteeringBehaviour
    {
        [SerializeField] private float _agentColliderSize;
        [SerializeField] private float _radius;
        public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
        {
            foreach (Collider2D obstacleCollider in aiData.obstacles)
            {
                Vector2 directionToObstacle
                    = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
                float distanceToObstacle = directionToObstacle.magnitude;

                //calculate weight based on the distance Enemy<--->Obstacle
                float weight
                    = distanceToObstacle <= _agentColliderSize
                    ? 1
                    : (_radius - distanceToObstacle) / _radius;

                Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

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
    }
}
