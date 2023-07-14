using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    internal class ParticlesUtil
    {
        public static GameObject SpawnParticles(GameObject particles, Transform origin)
        {
            var obj = UnityEngine.Object.Instantiate(particles, origin.position, Quaternion.identity);
            UnityEngine.Object.Destroy(obj, 5.0f);
            return obj;
        }
    }
}
