using Assets.Scripts.Entities.Player.Characters;
using Assets.Scripts.Entities.Stats.Interfaces;
using Assets.Scripts.Entities.Stats.Interfaces.StatCatchers;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Player.Skills.Implementations.Thors
{
	internal class ThorsShield : MonoBehaviour
	{
		[SerializeField] private GameObject _shieldObject;
		[SerializeField] private GameObject _shieldUI;
		[SerializeField] private TextMeshProUGUI _capacity;
		private AttributeMask _shieldMask;
		private Characters.Thors _entity;
		private IHealthChangedHandler _handler;
		private int _maxCapacity;
		private int _capacityRemain;
		private AdjustmentMask _damageMask = new(AdjustmentOperation.MULTIPLY, 0);
		private void Awake()
		{
			_entity = GetComponent<Characters.Thors>();
		}
		public void Activate(int maxDamageAbsorb, float addAttackSpeed)
		{
			_shieldObject.SetActive(true);
			_shieldUI.SetActive(true);
			(_entity as IDamageCorrector).OnDamageIncomed += OnDamage;
			_capacityRemain = maxDamageAbsorb;
			_maxCapacity = maxDamageAbsorb;
			_shieldMask = new AttributeMask() { MaskMultiplier = addAttackSpeed };
			_entity.Stats.Modify<AttackSpeedStat>(_shieldMask);
			(_entity as IDamageCorrector).Masks.Add(_damageMask);
			UpdateIndicator();
		}
		private void OnDamage(int rawDamage)
		{
			_capacityRemain -= rawDamage;
			UpdateIndicator();
			if(_capacityRemain <= 0)
			{
                (_entity as IDamageCorrector).OnDamageIncomed -= OnDamage;
                _entity.Stats.Unmodify<AttackSpeedStat>(_shieldMask);
                (_entity as IDamageCorrector).Masks.Remove(_damageMask);
                _shieldObject.SetActive(false);
				_shieldUI.SetActive(false);
			}
		}
		private void UpdateIndicator()
		{
			_capacity.text = $"{_capacityRemain}/{_maxCapacity}";
		}
	}
}
