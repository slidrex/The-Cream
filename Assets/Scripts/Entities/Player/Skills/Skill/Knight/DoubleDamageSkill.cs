using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Skill
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/Knight/Double Damage")]
    internal class DoubleDamageSkill : PlayerActiveSkill
    {
        [UnityEngine.SerializeField] private float duration;
        protected override void OnActivate(Player player)
        {
            player.Stats.ModifierHolder.AddModifier(new DoubleDamage(player, duration));
            Debug.Log("Double damage!");
        }
    }
}
