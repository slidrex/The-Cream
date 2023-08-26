using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.GameProgress;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Character
{
    [RequireComponent(typeof(Button))]
    internal class CharacterSelectButton : MonoBehaviour
    {
        [SerializeField] private Sprite _lockedCharacterIcon;
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

            button.onClick.AddListener(OnSelectButtonClicked);
        }
        public void OnSelectButtonClicked()
        {
            if (IsOpened)
            {
                Editor.Editor.SelectedCharacterId = CharacterID;
                podium.InitPodium(model.Character.Description.CharacterSprite, model.Character.Description.Name);
            }
            else podium.InitBlockedPodium();
            InitSkills();
        }

        public void InitSkills()
        {
            podium.ClearSkillDescripion();
            for (int i = 0; i < model.Character.GetSkills().Length; i++)
            {
                var button = podium.GetSkillButtons()[i];
                button.InitSkill(model.Character.GetSkills()[i], podium);

                button.SetSkillIcon(model.Character.GetSkills()[i].Icon);
                if (i == 0) button.UpdateSkillDescription();
                
            }
            StartCoroutine(podium.LayoutUpdater());
        }
        public void LockCharacter()
        {
            IsOpened = false;
            SetIcon(_lockedCharacterIcon);
        }
        public void InitiateCharacter(CharacterPodium podium, CharacterDatabaseModel.CharacterID id,
                                          CharacterDatabaseModel model, Sprite icon)
        {
            SetPodium(podium);
            SetCharacter(id, model);
            SetIcon(icon);
        }
        private void SetPodium(CharacterPodium podium)
        {
            this.podium = podium;
        }
        private void SetCharacter(CharacterDatabaseModel.CharacterID id,
                                          CharacterDatabaseModel model)
        {
            CharacterID = id;
            this.model = model;
        }
        private void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }
    }
}
