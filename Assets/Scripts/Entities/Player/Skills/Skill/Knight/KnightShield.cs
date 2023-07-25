using Assets.Scripts.Entities.Player.SpecialAbilities;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Skill.Knight
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/Knight/Crush Shield")]
    internal class KnightShield : PlayerActiveSkill
    {
        [UnityEngine.SerializeField] private Shield _shield;
        protected override void OnActivate(Player player)
        {
            var shield = Instantiate(_shield, player.transform.position, Quaternion.identity);
            shield.Configure(player);
        }
    }
}
