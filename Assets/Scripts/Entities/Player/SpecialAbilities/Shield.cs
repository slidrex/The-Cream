using Assets.Scripts.Entities.AI.SightStalking;
using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.SpecialAbilities
{
    internal class Shield : MonoBehaviour
    {
        [SerializeField] private GameObject _particles;
        [SerializeField] private float lifetime;
        [SerializeField] private int damage;
        private Entity _issuer;
        public void Configure(Entity entity)
        {
            _issuer = entity;
            transform.position = entity.transform.position + Vector3.right * entity.GetComponent<Facing>().CurrentSight;
            Destroy(gameObject, lifetime);
            ParticlesUtil.SpawnParticles(_particles, transform);
        }

    }
}
