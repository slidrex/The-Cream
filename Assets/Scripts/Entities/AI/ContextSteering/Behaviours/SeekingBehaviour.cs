using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering.Behaviours
{
    internal class SeekingBehaviour : SteeringBehaviour
    {
        private bool reachedLastTarget = true;


        [SerializeField] private float targetRechedThreshold = 0.5f;
        private Vector2 targetPositionCached;
        private float[] interestsTemp;
        public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
        {

                targetPositionCached = aiData.currentTarget.position;

            if (Vector2.SqrMagnitude(targetPositionCached - (Vector2)transform.position) < targetRechedThreshold * targetRechedThreshold)
            {
                reachedLastTarget = true;
                return (danger, interest);
            }

            Vector2 directionToTarget = targetPositionCached - (Vector2)transform.position;
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
            interestsTemp = interest;
            return (danger, interest);
        }
    }
}
