using Assets.Scripts.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.Databases.Model.Player
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/New character")]
    internal class CharacterModel : ScriptableObject
    {
        public Entities.Player.Player Player;
        public int ManaPool;
        private bool _configured;
        [SerializeField] private PlayerSkill[] _skills;
        public void ConfigureSkills()
        {
            if (_configured) return;
            _configured = true;
            Skills = new PlayerSkill[_skills.Length];
            for(int i = 0; i < _skills.Length; i++)
            {
                Skills[i] = Instantiate(_skills[i]);
            }
        }
        public PlayerSkill[] Skills { get; set; }
    }
}
