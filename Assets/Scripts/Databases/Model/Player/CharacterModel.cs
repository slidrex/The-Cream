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
        [field: SerializeField] public CharacterDescription Description { get; private set; } 
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
        public void OnAwake()
        {
            _configured = false;
        }
        public PlayerSkill[] GetSkills() => _skills;
        public PlayerSkill[] Skills { get; set; }
    }
}
