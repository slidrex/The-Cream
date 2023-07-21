using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Entities.Reset
{
    internal interface IResettable
    {
        /// <summary>
        /// Alternative start for Entity.
        /// </summary>
        void OnReset();
    }
}
