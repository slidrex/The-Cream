using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LevelEditor.RuntimeSpace.Player
{
    internal class PlayerRuntimeSpaceView : MonoBehaviour
    {
        [UnityEngine.SerializeField] private TextMeshProUGUI _manaText;
        [UnityEngine.SerializeField] private Image _fill;
        public void SetMana(int currentMana, int maxMana)
        {
            _manaText.text = currentMana.ToString();
            _fill.fillAmount = (float)currentMana / maxMana;
        }
    }
}
