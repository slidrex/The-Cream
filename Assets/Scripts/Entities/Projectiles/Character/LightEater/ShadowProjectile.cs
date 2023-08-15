using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Entities.Projectiles.Character.LightEater
{
    internal class ShadowProjectile : AttackProjectile<Entities.Player.Characters.LightEater>
    {
        private float _directionTime;
        private float _timeSinceShoot;
        public override EntityTypeBase TriggerEntityType => new EntityType<MobTag>().Any();
        public void InitTime(float directionTime)
        {
            _directionTime = directionTime;
        }
        private void Update()
        {
            OnBeforeReturn();
        }
        private void OnBeforeReturn()
        {
            if(_timeSinceShoot < _directionTime)
            {
                _timeSinceShoot += Time.deltaTime;
                if(_timeSinceShoot >= _directionTime)
                {
                    SetMoveDirection(-MoveVector);
                }
            }
        }
    }
}
