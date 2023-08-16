using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Skills;
using Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Interfaces;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

internal class SkillHolder : ObjectHolder
{
    [SerializeField] private Image cooldownAnim;
    private ICooldownable _skill;
    public void Init<T>(PlayerSkill skill, T player, KeyCode bindedKey) where T : Player
    {
        SetActiveSelectImage(false);
        SetBindedKey(bindedKey, skill is IActivatable);

        EntityIcon.sprite = skill.Icon;
        button = GetComponent<Button>();
        skill.OnStart(player);
        _skill = skill as ICooldownable;
        if(skill is IActivatable active)
        {
            button.onClick.AddListener(delegate { active.TryActivate(this, player, true); });
            Assets.Scripts.Entities.Util.Config.Input.InputManager.Bind(bindedKey, () => active.TryActivate(this, player, false));
            Cost.text = active.BaseManacost.ToString();
        }
    }
    private void Update()
    {
        if(_skill != null)
        {
            cooldownAnim.fillAmount = 1 - _skill.TimeSinceActivation/_skill.BaseCooldown;
        }
    }
}
