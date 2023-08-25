using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.Menu.Character;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.GameProgress
{
    internal static class PersistentData
    {
        public static int CurrentGameLevel = 1;
        public static HashSet<CharacterDatabaseModel.CharacterID> UnlockedCharacters = new () { CharacterDatabaseModel.CharacterID.THORS };
    }
}
