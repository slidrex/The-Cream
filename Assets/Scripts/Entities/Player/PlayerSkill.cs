using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    [CreateAssetMenu(menuName = "Cream/Database/Character/New skill")]
    internal class PlayerSkill : ScriptableObject
    {
        public Sprite Icon;
        public bool IsPassive;
        public int BaseManacost;
        public float BaseCooldown;
    }
}
