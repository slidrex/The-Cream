using Assets.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.UI;

internal class SkillSelectButton : MonoBehaviour
{
    [SerializeField] private Sprite _unknownSkillIcon;
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
    public void InitSkill(PlayerSkill skill, CharacterPodium podium)
    {
        gameObject.SetActive(true);
        this.skill = skill;
        if(this.podium == null) this.podium = podium;
    }
    public void SetSkillIcon(Sprite icon)
    {
        this.icon.sprite = icon;
    }
}
