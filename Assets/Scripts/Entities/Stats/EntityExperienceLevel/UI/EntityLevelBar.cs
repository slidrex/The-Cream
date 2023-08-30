using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.EntityExperienceLevel.UI
{
    internal class EntityLevelBar : MonoBehaviour
    {
        [SerializeField] private Image _expValue;
        [SerializeField] private TextMeshProUGUI _level;
        public void UpdateBar(ILevelEntity levelEntity)
        {
            _level.text = levelEntity.CurrentLevel.ToString();
            _expValue.fillAmount = (float)levelEntity.CurrentExp/levelEntity.GetLevelExperienceCost(levelEntity.CurrentLevel + 1);
        }
    }
}