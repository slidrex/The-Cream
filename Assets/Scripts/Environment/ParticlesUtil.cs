using UnityEngine;

namespace Assets.Scripts.Environment
{
    internal class ParticlesUtil
    {
        public static GameObject SpawnParticles(GameObject particles, Transform origin)
        {
            var obj = Object.Instantiate(particles, origin.position, Quaternion.identity);
            Object.Destroy(obj, obj.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
            return obj;
        }
    }
}
