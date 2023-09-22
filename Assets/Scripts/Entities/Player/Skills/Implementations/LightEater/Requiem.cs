using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Player.Characters;
using Assets.Scripts.Entities.Projectiles;
using Assets.Scripts.Entities.Projectiles.Character.LightEater;
using Assets.Scripts.Entities.Stats.StatAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Entities.Stats.Interfaces;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Entities.Player.Skills.Implementations.LightEater
{
    internal class Requiem : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private float _oneDirectionFlyTime;
        [SerializeField] private byte _projectileCount;
        [SerializeField] private ShadowProjectile _projectile;
        [SerializeField] private string _abilityTrigger;
        private Movement _entityMovement;
        private Characters.LightEater _entity;
        private AttributeMask _speedMask = new AttributeMask() { MaskMultiplier = -1 };
        private IMutable _mutable;
        private void Awake()
        {
            _entity = GetComponent<Characters.LightEater>();
            _mutable = _entity;
            _entityMovement = GetComponent<Movement>();
            _animator = GetComponent<Animator>();
        }
        public void StartAbility()
        {
            _animator.SetTrigger(_abilityTrigger);
            _mutable.IsMuted = true;
            _entityMovement.EnableMovement(false);
        }
        public void PerformAbility()
        {
            for(int i = 0; i  <= 360; i += 360/_projectileCount)
            {
                Vector2 dir = new Vector2(Mathf.Cos(i), Mathf.Sin(i)); 
                var obj = Instantiate(_projectile, transform.position, Quaternion.Euler(0, 0, i));
                obj.SetMoveDirection(obj.transform.up);
                obj.SetOwner(_entity);
                obj.InitTime(_oneDirectionFlyTime);
            }
            _mutable.IsMuted = false;
            _entityMovement.EnableMovement(true);
        }
    }
}
