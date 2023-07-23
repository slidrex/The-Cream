using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.GameProgress.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Character
{
    [RequireComponent(typeof(Button))]
    internal class CharacterSelectButton : MonoBehaviour
    {
        public CharacterDatabaseModel.CharacterID CharacterID;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => CharacterPrefs.SelectPlayer(CharacterID));
        }
    }
}
