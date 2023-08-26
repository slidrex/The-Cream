using Assets.Scripts.Databases.Character;
using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.Menu.Character;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.GameProgress;
using UnityEngine.Localization.Components;

public class CharacterPodium : MonoBehaviour
{
    [Header("Locked Podium")]
    [SerializeField] private Sprite _lockedPodiumSprite;
    [SerializeField] private string _lockedPodiumName;
    [Header("General")]
    [SerializeField] private CharacterSelectButton characterButton;
    [SerializeField] private CharacterDatabase database;
    [SerializeField] private RectTransform DescriptionObject;

    [Header("SkillDescription")]
    [SerializeField] private LocalizeStringEvent _descriptionLocalizer;
	[SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private Transform skillsContainer;

    [Header("Podium")]
    [SerializeField] private Transform characterContainer;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterIcon;

    private CharacterDatabaseModel[] models;
    private SkillSelectButton[] skillButtons;

    private void Start()
    {
        skillButtons = skillsContainer.GetComponentsInChildren<SkillSelectButton>();
        for (int i = 0; i < skillButtons.Length; i++)
            skillButtons[i].gameObject.SetActive(false);

        InstantiateCharacters();
    }
    private void InstantiateCharacters()
    {
        models = database.GetModels();
        CharacterDatabaseModel.CharacterID[] ids =
            (CharacterDatabaseModel.CharacterID[])Enum.GetValues(typeof(CharacterDatabaseModel.CharacterID));
        bool _initialCharacterFound = false;
        for (int i = 0; i < models.Length; i++)
        {
            models[i].Character.Description.Init(models[i].Character.GetSkills());
            var but = Instantiate(characterButton, characterContainer);
            
            but.InitiateCharacter(this, ids[i], models[i], models[i].Character.Description.CharacterButtonIcon);
            if (PersistentData.UnlockedCharacters.Contains(ids[i]) == false)
            {
                but.LockCharacter();
            }
            else if(_initialCharacterFound == false)
            {
                var desc = models[i].Character.Description;

                but.OnSelectButtonClicked();

				_initialCharacterFound = true;
			}
        }
    }
    public void ClearSkillDescripion()
    {
        for(int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i].gameObject.SetActive(false);
        }
        skillName.text = "...";
        skillDescription.text = "...\n" +
                                "...\n" +
                                "...";
    }
    public void InitPodium(Sprite icon, string name)
    {
        characterIcon.sprite = icon;
        characterName.text = name;
    }
    public void InitBlockedPodium()
    {
        InitPodium(_lockedPodiumSprite, _lockedPodiumName);
    }
    public Transform GetSkillContainer() => skillsContainer;
    public void SetPodiumSkill(string name, string descriptionKey) 
    {
		skillName.text = name;
        _descriptionLocalizer.SetEntry(descriptionKey);
        _descriptionLocalizer.RefreshString();
	}
    internal SkillSelectButton[] GetSkillButtons() => skillButtons;
    public IEnumerator LayoutUpdater()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(DescriptionObject);
    }
}
