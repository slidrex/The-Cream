using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.Menu.Character;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.GameProgress
{
    internal static class PersistentData
    {
        public static bool IsNewbie = true;
        public static int CurrentGameLevel = 5;
        public static int SelectedLanguageIndex;
        public static bool IsTutorialPassed;
        public static HashSet<CharacterDatabaseModel.CharacterID> UnlockedCharacters = new () { CharacterDatabaseModel.CharacterID.KNIGHT, CharacterDatabaseModel.CharacterID.THORS, CharacterDatabaseModel.CharacterID.LIGHT_EATER };
    }
}
