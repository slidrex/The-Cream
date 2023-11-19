using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.GameProgress.DTO;
using Newtonsoft.Json;

namespace GameProgress
{
    public static class PersistentData
    {
        public static void LoadData(string data)
        {
            IsLoaded = true;
            if ( string.IsNullOrEmpty(data))
            {
                
                return;
            }

            var dataObj = Newtonsoft.Json.JsonConvert.DeserializeObject<PersistentDataModel>(data);
            CurrentGameLevel = dataObj.CurrentGameLevel;
            SelectedLanguageIndex = dataObj.SelectedLanguageIndex;
            IsTutorialPassed = dataObj.IsTutorialPassed;
            UnlockedCharacters = new HashSet<CharacterDatabaseModel.CharacterID>();
            UnlockedCharacters = dataObj.UnlockedCharacters.Select(x => (CharacterDatabaseModel.CharacterID)x).ToHashSet();
            IsNewbie = dataObj.IsNewbie;
        }

        public static string GenerateData()
        {
            var model = new PersistentDataModel();
            model.CurrentGameLevel = CurrentGameLevel;
            model.SelectedLanguageIndex = SelectedLanguageIndex;
            model.IsTutorialPassed = IsTutorialPassed;
            model.UnlockedCharacters = UnlockedCharacters.Select(x => (int)x);
            return JsonConvert.SerializeObject(model);
        }

        public static bool IsLoaded = false;
        public static bool IsNewbie = true;
        public static int CurrentGameLevel = 1;
        public static int SelectedLanguageIndex;
        public static bool IsTutorialPassed;
        public static HashSet<CharacterDatabaseModel.CharacterID> UnlockedCharacters = new HashSet<CharacterDatabaseModel.CharacterID>()
            {
                CharacterDatabaseModel.CharacterID.KNIGHT
            };
    }
}
