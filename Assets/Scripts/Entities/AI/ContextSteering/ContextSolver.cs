using Assets.Scripts.Entities.AI.ContextSteering.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering
{
    [RequireComponent(typeof(ObstacleBehavour))]
    [RequireComponent(typeof(SeekingBehaviour))]
    [RequireComponent(typeof(AIData))]
    [RequireComponent(typeof(AIDataLoader))]
    internal class ContextSolver : MonoBehaviour
    {
        private Vector2 resultDirection;
        private Rigidbody2D rb;
        private SeekingBehaviour seekingBehaviour;
        private AIData data;
        private AIDataLoader loader;
        private ObstacleBehavour obstacleBehavour;
        private void Start()
        {
            obstacleBehavour = GetComponent<ObstacleBehavour>();
            seekingBehaviour = GetComponent<SeekingBehaviour>();
            loader = GetComponent<AIDataLoader>();
            data = GetComponent<AIData>();
            rb = GetComponent<Rigidbody2D>();
        }
        public Vector2 GetDirectionToMove(AIData aiData, params SteeringBehaviour[] behaviours)
        {
            float[] danger = new float[8];
            float[] interest = new float[8];

            //Loop through each behaviour
            foreach (SteeringBehaviour behaviour in behaviours)
            {
                (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
            }

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

            resultDirection = outputDirection;

            //return the selected movement direction
            return resultDirection;
        }
        private void Update()
        {
            rb.velocity = GetDirectionToMove(data, seekingBehaviour, obstacleBehavour);
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
