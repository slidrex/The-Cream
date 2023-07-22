using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Skills;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

internal class SkillHolder : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Button button;

    public void Init(PlayerSkill skill, Player player)
    {
        icon.sprite = skill.Icon;
        button = GetComponent<Button>();
        skill.OnStart(player);
        if (skill is PlayerActiveSkill active)
        {
            button.onClick.AddListener(delegate { active.TryActivate(player); });
        }
        
    }
}
