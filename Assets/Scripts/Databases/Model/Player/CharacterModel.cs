using Assets.Scripts.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.Databases.Model.Player
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/New character")]
    internal class CharacterModel : ScriptableObject
    {
        public Entities.Player.Player Player;
        public int ManaPool;
        public PlayerSkill[] Skills;
    }
}
