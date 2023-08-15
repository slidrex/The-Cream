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
        private void Awake()
        {
            _entity = GetComponent<Characters.LightEater>();
            _entityMovement = GetComponent<Movement>();
            _animator = GetComponent<Animator>();
        }
        public async void StartAbility()
        {
            _entityMovement.EnableMovement(false);
            await Task.Delay(2000);
            PerformAbility();
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
            _entityMovement.EnableMovement(true);
        }
    }
}
