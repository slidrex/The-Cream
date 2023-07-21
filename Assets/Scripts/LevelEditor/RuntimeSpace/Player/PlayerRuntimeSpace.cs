using Assets.Scripts.Databases.dto;
using Assets.Scripts.Databases.dto.Units;
using Assets.Scripts.Databases.Model.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LevelEditor.RuntimeSpace.Player
{
    internal class PlayerRuntimeSpace : MonoBehaviour
    {
        [SerializeField] private CharacterModel _character;
        [SerializeField] private PlayerRuntimeSpaceView _view;
        public int CurrentMana { get; private set; }
        public Assets.Scripts.Entities.Player.Player InitPlayer(Vector2 position)
        {
            SetMana(_character.ManaPool);
            return Instantiate(_character.Player, position, Quaternion.identity);
        }
        public void OnConfigure()
        {
            _character.OnAwake();
        }
        public List<PlayerSkillModel.Model> GetPlayerSkillModels()
        {
            _character.ConfigureSkills();
            var models = new List<PlayerSkillModel.Model>();

            foreach(var skill in _character.Skills)
            {
                models.Add(new PlayerSkillModel.Model(skill));
            }
            return models;
        }
        public bool TrySpendMana(int mana)
        {
            if (mana > CurrentMana) return false;
            else SetMana(CurrentMana - mana);
            return true;
        }
        private void SetMana(int mana)
        {
            CurrentMana = mana;
            _view.SetMana(CurrentMana, _character.ManaPool);
        }
        public CharacterModel GetCharacterModel() => _character;
    }
}
