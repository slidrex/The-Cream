using Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Interfaces;
using UnityEngine;
using UnityEngine.UI;

internal class RuntimeEntityHolder : ObjectHolder
{
    [SerializeField] private Image _cooldownMask;
    private ICooldownable _cooldownable;
    public void Configure(int manacost, ICooldownable cooldownable)
    {
        _cooldownable = cooldownable;
        Cost.text = manacost.ToString();
    }
    public void UpdateCooldownValue()
    {
        _cooldownMask.fillAmount = 1 - (_cooldownable.TimeSinceActivation / _cooldownable.BaseCooldown);
    }
}
