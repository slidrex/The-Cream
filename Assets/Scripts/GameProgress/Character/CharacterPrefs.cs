using Assets.Scripts.Databases.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameProgress.Character
{
    internal class CharacterPrefs
    {
        public static void SelectPlayer(CharacterDatabaseModel.CharacterID characterID)
        {
            PlayerPrefs.SetInt(PrefsKey.SELECTED_CHARACTER, (int)characterID);
        }
    }
}
