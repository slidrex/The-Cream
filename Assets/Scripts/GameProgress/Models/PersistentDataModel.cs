using System.Collections.Generic;
using Assets.Scripts.Databases.Model.Character;

namespace Assets.Scripts.GameProgress.DTO
{
    public class PersistentDataModel
    {
        public bool IsNewbie = true;
        public int CurrentGameLevel = 1;
        public int SelectedLanguageIndex;
        public bool IsTutorialPassed;
        public IEnumerable<int> UnlockedCharacters = new List<int> { 0 };
    }
}