﻿using Assets.Scripts.Databases.Model.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Databases.Model.Character
{
    [Serializable]
    public class CharacterDatabaseModel
    {
        public enum CharacterID
        {
            KNIGHT,
            THORS,
            LIGHT_EATER
        }
        [field: SerializeField] public CharacterID Id { get; set; }
        [field: SerializeField] public CharacterModel Character { get; set; }
    }
}
