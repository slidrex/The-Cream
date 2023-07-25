using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering
{
    internal class AIData : MonoBehaviour
    {
        public Collider2D[] obstacles = null;

        public Transform currentTarget;
    }
}
