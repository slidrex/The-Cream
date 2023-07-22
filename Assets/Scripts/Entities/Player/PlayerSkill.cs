using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    internal abstract class PlayerSkill : ScriptableObject
    {
        public Sprite Icon;
        public virtual void OnStart(Player player)
        {
            
        }
        public virtual void Update()
        {
            
        }
    }
}
