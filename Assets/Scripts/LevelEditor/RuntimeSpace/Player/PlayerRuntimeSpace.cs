using Assets.Scripts.Databases.dto;
using Assets.Scripts.Databases.dto.Units;
using Assets.Scripts.Databases.Model.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.LevelEditor.RuntimeSpace.Player
{
    internal class PlayerRuntimeSpace : MonoBehaviour
    {
        [SerializeField] private PlayerRuntimeSpaceView _view;
        private CharacterModel _character;
        public int CurrentMana { get; private set; }
        public void InitPlayer(CharacterModel character)
        {
            _character = character;
        }
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
        public bool IsEnoughMana(int mana)
        {
            return mana <= CurrentMana;
        }
        /// <summary>
        /// Throws exception if set mana is below zero.
        /// </summary>
        public void SpendMana(int mana)
        {
            if (mana > CurrentMana) throw new Exception("Mana set below zero.");
            else SetMana(CurrentMana - mana);
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
