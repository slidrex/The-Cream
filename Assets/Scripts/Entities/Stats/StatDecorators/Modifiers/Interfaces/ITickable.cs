using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces
{
    internal interface ITickable : IDurationable
    {
        float CallInterval { get; set; }
        /// <summary>
        /// Calls every <see cref="CallInterval"/> seconds during <see cref="IDurationable.Duration"/>
        /// </summary>
        void OnTick();
    }
}
