using Assets.Scripts.Entities.Player.Skills.Implementations.LightEater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.LightEater
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/LightEater/Shadow Coil")]
    internal class ShadowCoil : PlayerDirectSkill<Characters.LightEater>
    {
        [SerializeField] private CoilProjectile _coil;
        [SerializeField] private float _castDistance = 3.0f;
        protected override float MaxCastDistance { get => _castDistance; }
        protected override void OnActivate(Vector2 mousePos, Characters.LightEater player)
        {
            player.GetComponent<ShadowCoilActivate>().CoilStart(mousePos, _coil);
        }
    }
}
