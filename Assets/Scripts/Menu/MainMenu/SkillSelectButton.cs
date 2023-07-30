using Assets.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.UI;

internal class SkillSelectButton : MonoBehaviour
{
    private Image icon;
    private Button button;
    private PlayerSkill skill;
    private CharacterPodium podium;
    private void Awake()
    {
        button = GetComponent<Button>();
        icon = GetComponent<Image>();
        button.onClick.AddListener(UpdateSkillDescription);
    }
    private void UpdateSkillDescription()
    {
        podium.SetSkillName(skill.Description.Name);
        podium.SetSkillDescription(skill.Description.Description);
        StartCoroutine(podium.LayoutUpdater());
    }
    public void Init(PlayerSkill skill, Sprite icon, CharacterPodium podium)
    {
        gameObject.SetActive(true);
        this.skill = skill;
        this.icon.sprite = icon;
        if(this.podium == null) this.podium = podium;
    }
}
