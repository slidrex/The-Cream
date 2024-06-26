﻿using Assets.Scripts.PlatformConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GameProgress.DTO;
using GameProgress;
using UnityEngine.Analytics;

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
                    case 3:
                    {
                        #if !UNITY_EDITOR && UNITY_WEBGL
                        Yandex.Instance.RateGameButton();
#endif 
                        Yandex.Instance.RateGameButton();
                        PersistentData.UnlockedCharacters.Add(Databases.Model.Character.CharacterDatabaseModel.CharacterID.LIGHT_EATER);
                        break;
                    }
            }
            PersistentData.CurrentGameLevel = level + 1;

            Yandex.Instance.SaveExternData();
            Yandex.Instance.ShowAdvert();
			Analytics.CustomEvent("level_passed", new Dictionary<string, object>() { ["level_index"] = level});
        }
    }
}
