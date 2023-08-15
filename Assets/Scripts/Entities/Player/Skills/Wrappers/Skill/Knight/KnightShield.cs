using Assets.Scripts.Entities.Player.Characters;
using Assets.Scripts.Entities.Player.Skills.Wrappers;
using Assets.Scripts.Entities.Player.SpecialAbilities;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Skill.Knight
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/Skill/Knight/Crush Shield")]
    internal class KnightShield : PlayerUndirectSkill<Characters.Knight>
    {
        [UnityEngine.SerializeField] private Shield _shield;
        protected override void OnActivate(Characters.Knight player)
        {
            var shield = Instantiate(_shield, player.transform.position, Quaternion.identity);
            shield.Configure(player);
        }
    }
}
