﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameProgress
{
    internal static class LevelProgressResolver
    {
        private static List<int> PassedLevels = new();
        public static void PassLevel(int level)
        {
            if (PassedLevels.Contains(level) == false)
            {
                PassedLevels.Add(level);
                OnLevelPassed(level);
            }
        }
        private static void OnLevelPassed(int level)
        {
            switch(level)
            {
                case 1: 
                    {
                        PersistentData.UnlockedCharacters.Add(Databases.Model.Character.CharacterDatabaseModel.CharacterID.THORS);
                        break;
                    }
                    case 2:
                    {
                        PersistentData.UnlockedCharacters.Add(Databases.Model.Character.CharacterDatabaseModel.CharacterID.LIGHT_EATER);
                        break;
                    }
            }
            PersistentData.CurrentGameLevel = level + 1;
        }
    }
}
