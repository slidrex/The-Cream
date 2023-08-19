using Assets.Scripts.Entities.Player.Skills.Implementations.Knight;
using Assets.Scripts.Entities.Player.Skills.Wrappers;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Skill
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/Knight/Double Damage")]
    internal class DoubleDamageSkill : PlayerUndirectSkill<Characters.Knight>
    {
        [SerializeField] private float duration;
        protected override void OnActivate(Characters.Knight player)
        {
            var ddParticles = player.GetComponent<ParticleGenerator>();
            ddParticles.Enable(true);
            player.Stats.ModifierHolder.AddModifier(new DoubleDamage(player, duration, () => ddParticles.Enable(false)));
        }
    }
}
