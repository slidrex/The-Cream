using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class SkillHolder : ObjectHolder
{
    [SerializeField] private Image cooldownAnim;
    private float cooldown = 0;
    private float timeToActivate = 0;
    private bool isCooldown = false;
    public void Init(PlayerSkill skill, Player player)
    {
        EntityIcon.sprite = skill.Icon;
        button = GetComponent<Button>();
        skill.OnStart(player);
        if (skill is PlayerActiveSkill active)
        {
            button.onClick.AddListener(delegate { active.TryActivate(player); });
            button.onClick.AddListener(OnActivate);
            Cost.text = active.BaseManacost.ToString();
            cooldown = active.BaseCooldown;
        }
    }
    private void OnActivate()
    {
        isCooldown = true;
    }
    private void Update()
    {
        if(isCooldown == true && timeToActivate < cooldown)
        {
            timeToActivate += Time.deltaTime;
            cooldownAnim.fillAmount = timeToActivate / cooldown;
        }
        else if(timeToActivate >= cooldown)
        {
            isCooldown = false;
            timeToActivate = 0;
            cooldownAnim.fillAmount = 0;
        }
    }

}
