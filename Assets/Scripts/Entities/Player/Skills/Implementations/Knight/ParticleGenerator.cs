using Assets.Scripts.Entities.Brain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Implementations.Knight
{
    internal class ParticleGenerator : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;
        public void Enable(bool active)
        {
            _particles.gameObject.SetActive(active);
            if (active) _particles.Play();
            else _particles.Stop();
        }
    }
}
