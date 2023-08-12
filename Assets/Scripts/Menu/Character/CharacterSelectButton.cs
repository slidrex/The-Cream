using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.GameProgress.Character;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Character
{
    [RequireComponent(typeof(Button))]
    internal class CharacterSelectButton : MonoBehaviour
    {
        [SerializeField] private SkillSelectButton skillButton;
        [field: SerializeField] public bool IsOpened { get; private set; } = true;
        private Image icon;
        public CharacterDatabaseModel.CharacterID CharacterID;
        private CharacterDatabaseModel model;
        private CharacterPodium podium;
        private Button button;
        private void Awake()
        {
            icon = GetComponent<Image>();
            button = GetComponent<Button>();
            if (IsOpened)
            {
                button.onClick.AddListener(() => CharacterPrefs.SelectPlayer(CharacterID));
                button.onClick.AddListener(() => podium.InitPodium(model.Character.Description.CharacterSprite, model.Character.Description.Name));
                button.onClick.AddListener(InitSkills);
            }
        }

        private void InitSkills()
        {
            podium.ClearSkillDescripion();
            for (int i = 0; i < model.Character.GetSkills().Length; i++)
            {
                podium.GetSkillButtons()[i].Init(model.Character.GetSkills()[i], model.Character.GetSkills()[i].Icon, podium);
            }
            StartCoroutine(podium.LayoutUpdater());
        }
        public void SetPodium(CharacterPodium podium)
        {
            this.podium = podium;
        }
        public void SetCharacter(CharacterDatabaseModel.CharacterID id,
                                          CharacterDatabaseModel model)
        {
            CharacterID = id;
            this.model = model;
        }
        public void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }
    }
}
