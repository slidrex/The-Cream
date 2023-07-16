using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
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
            _barObject.gameObject.SetActive(true);
            var healthChangedHandler = entity as IHealthChangedHandler;
            _barFill.color = bossbar.BarColor;
            if (healthChangedHandler == null) throw new NullReferenceException("Entity" + entity.name + " must have IHealthChangedHandler interface.");
            healthChangedHandler.OnHealthChanged += (int newHealth) => OnHealthChanged(entity as IDamageable);
        }
        private void OnHealthChanged(IDamageable health)
        {
            _barFill.fillAmount = (float)health.CurrentHealth/health.MaxHealth;
            if (health.CurrentHealth <= 0) DisableBar(); 
        }
        public void DisableBar()
        {
            _barObject.gameObject.SetActive(false);
        }
    }
}
