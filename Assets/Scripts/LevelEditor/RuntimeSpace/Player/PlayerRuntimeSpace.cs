using Assets.Scripts.Databases.dto;
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
        public int CurrentMana { get; private set; }
        public List<RuntimeEntityModel.Model> GetPlayerSkillModels()
        {
            var models = new List<RuntimeEntityModel.Model>();
            foreach(var skill in _character.Skills)
            {
                models.Add(new RuntimeEntityModel.Model(skill));
            }
            return models;
        }
    }
}
