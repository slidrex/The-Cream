using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.Databases.Model.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Databases.Character
{
    [CreateAssetMenu(menuName = "Cream/Database/New Character Database")]
    internal class CharacterDatabase : ScriptableObject
    {
        [SerializeField] private CharacterDatabaseModel[] _models;
        public CharacterModel GetCharacter(CharacterDatabaseModel.CharacterID id)
        {
            foreach (var model in _models)
            {
                if (model.Id == id) return model.Character;
            }
            throw new NullReferenceException();
        }
    }
}
