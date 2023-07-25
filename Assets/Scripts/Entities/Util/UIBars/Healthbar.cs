using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Util.UIBars
{
    internal class Healthbar : MonoBehaviour
    {
        [SerializeField] private Image _barValue;
        [SerializeField] private TextMeshProUGUI _barText;
        [SerializeField] private Entity _entity;
        private IDamageable _damageComponent;
        private MaxHealthStat _maxHealthStat;
        private IHealthChangedHandler _changeHealthHandler;
        private void Awake()
        {
            _changeHealthHandler = _entity as IHealthChangedHandler;
            _damageComponent = _entity as IDamageable;
            _maxHealthStat = _entity.Stats.GetAttribute<MaxHealthStat>();
            if (_damageComponent == null || _changeHealthHandler == null) throw new NullReferenceException("Entity " + _entity.name + " has missing interfaces. Please, make sure that entity has IDamageable and IChangeHealthHandler interfaces.");
            _changeHealthHandler.OnHealthChanged += UpdateBar;
        }
        private void UpdateBar(int newHealth, int oldHealth, Entity dealer)
        {
            if (_barText != null) _barText.text = _damageComponent.CurrentHealth.ToString();
            _barValue.fillAmount = _damageComponent.CurrentHealth/ _maxHealthStat.GetValue();
        }
    }
}
