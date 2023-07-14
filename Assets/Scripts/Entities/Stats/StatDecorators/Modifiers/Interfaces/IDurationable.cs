using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces
{
    internal interface IDurationable
    {
        public float Duration { get; set; }
        /// <summary>
        /// Calls when effect is over.
        /// </summary>
        public void OnEffectEnd();
    }
}
