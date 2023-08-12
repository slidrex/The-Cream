using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class SkillHolder : ObjectHolder
{
    [SerializeField] private Image cooldownAnim;
    private PlayerActiveSkill _skill;
    public void Init(PlayerSkill skill, Player player, KeyCode bindedKey)
    {
        SetActiveSelectImage(false);
        SetBindedKey(bindedKey, skill is PlayerActiveSkill == true);
        EntityIcon.sprite = skill.Icon;
        button = GetComponent<Button>();
        skill.OnStart(player);
        if (skill is PlayerActiveSkill active)
        {
            _skill = active;
            button.onClick.AddListener(delegate { active.TryActivate(this, player); });
            Assets.Scripts.Entities.Util.Config.Input.InputManager.Bind(bindedKey, () => active.TryActivate(this, player));
            Cost.text = active.BaseManacost.ToString();
        }
        else _skill = null;
    }
    private void Update()
    {
        if(_skill != null)
        {
            cooldownAnim.fillAmount = 1 - _skill.TimeSinceActivation/_skill.BaseCooldown;
        }
    }

}
