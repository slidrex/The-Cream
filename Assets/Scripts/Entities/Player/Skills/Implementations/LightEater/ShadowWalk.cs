using Assets.Scripts.Entities.Stats.Interfaces.Attack;
using Assets.Scripts.Entities.Stats.Interfaces.Detect;
using Assets.Scripts.Entities.Stats.StatAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Implementations.LightEater
{
    internal class ShadowWalk : MonoBehaviour
    {
        private float _remainTime;
        [SerializeField] private Color _walkColor;
        private Characters.LightEater _player;
        private AttributeMask _speedMask = new AttributeMask() { MaskMultiplier = 0.5f };
        public void StartWalk(float duration, Characters.LightEater player)
        {
            _remainTime = duration;
            _player = player;
            player.Stats.Modify<SpeedStat>(_speedMask);
            player.SpriteRenderer.color = _walkColor;
            (player as IAttackMutable).MutedAttack = true;
            (player as IUndetectable).IsUndetectable = true;
        }
        private void StopWalk()
        {
            _player.Stats.Unmodify<SpeedStat>(_speedMask);
            _player.SpriteRenderer.color = _player.DefaultColor;
            (_player as IAttackMutable).MutedAttack = false;
            (_player as IUndetectable).IsUndetectable = false;
        }
        private void Update()
        {
            if(_remainTime > 0)
            {
                _remainTime -= Time.deltaTime;
                if(_remainTime <= 0)
                {
                    StopWalk();
                }
            }
        }
    }
}
