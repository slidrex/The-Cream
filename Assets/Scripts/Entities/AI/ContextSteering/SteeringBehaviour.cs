using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering
{
    internal abstract class SteeringBehaviour : MonoBehaviour
    {
        public abstract (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData);
    }
}
