using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Stage;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Brain.Boss.Bossbar
{
    internal class BossbarController : MonoBehaviour, IResettable
    {
        [SerializeField] private LocalizeStringEvent _displayName;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private GameObject _barObject;
        [SerializeField] private Image _barFill;
        public void EnableBar(EntityBossbar bossbar, Entity entity, string displayName)
        {
            _displayName.SetEntry(displayName);
            _displayName.RefreshString();
            if (entity.HousingElement != StageController.Singleton._currentElement) return;
            _barObject.gameObject.SetActive(true);

            var healthChangedHandler = entity as IHealthChangedHandler;
            _barFill.color = bossbar.BarColor;

            _name.color = bossbar.BarColor;
            var h = entity as IDamageable;
            var maxHealthStat = entity.Stats.GetValueInt<MaxHealthStat>();

            if (healthChangedHandler == null) throw new NullReferenceException("Entity" + entity.name + " must have IHealthChangedHandler interface.");
            
            _barFill.fillAmount = (float)h.CurrentHealth / maxHealthStat;
            healthChangedHandler.OnHealthChanged += (int oldHealth, int newHealth, Entity dealer) => OnHealthChanged(h, maxHealthStat);
        }
        private void OnHealthChanged(IDamageable health, int maxHealth)
        {
            _barFill.fillAmount = (float)health.CurrentHealth/ maxHealth;
            if (health.CurrentHealth <= 0) DisableBar(); 
        }
        public void DisableBar()
        {
            _barObject.gameObject.SetActive(false);
        }

        public void OnReset()
        {
            DisableBar();
        }
    }
}
