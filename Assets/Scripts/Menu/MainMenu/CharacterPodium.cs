using Assets.Scripts.Databases.Character;
using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.Menu.Character;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterPodium : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CharacterSelectButton characterButton;
    [SerializeField] private CharacterDatabase database;
    [SerializeField] private RectTransform DescriptionObject;

    [Header("SkillDescription")]
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

        for (int i = 0; i < models.Length; i++)
        {
            models[i].Character.Description.Init(models[i].Character.GetSkills());
            var but = Instantiate(characterButton, characterContainer);
            but.SetPodium(this);
            but.SetCharacter(ids[i], models[i]);
            but.SetIcon(models[i].Character.Description.CharacterIcon);
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
    public Transform GetSkillContainer() => skillsContainer;
    public void SetSkillName(string name) => skillName.text = name;
    public void SetSkillDescription(string description) => skillDescription.text = description;
    internal SkillSelectButton[] GetSkillButtons() => skillButtons;
    public IEnumerator LayoutUpdater()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(DescriptionObject);
    }
}
