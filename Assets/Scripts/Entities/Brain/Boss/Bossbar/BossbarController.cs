using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Brain.Boss.Bossbar
{
    internal class BossbarController : MonoBehaviour
    {
        [SerializeField] private GameObject _barObject;
        [SerializeField] private Image _barFill;
        private Entity _entity;
        public void EnableBar(EntityBossbar bossbar, Entity entity)
        {
            if (entity.HousingElement != StageController.Singleton._currentElement) return;
            _barObject.gameObject.SetActive(true);
            var healthChangedHandler = entity as IHealthChangedHandler;
            _barFill.color = bossbar.BarColor;
            if (healthChangedHandler == null) throw new NullReferenceException("Entity" + entity.name + " must have IHealthChangedHandler interface.");
            healthChangedHandler.OnHealthChanged += (int oldHealth, int newHealth, Entity dealer) => OnHealthChanged(entity as IDamageable, entity.Stats.GetValueInt<MaxHealthStat>());
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
    }
}
