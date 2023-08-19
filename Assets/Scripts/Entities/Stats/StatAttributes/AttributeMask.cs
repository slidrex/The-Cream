using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatAttributes
{
    internal class AttributeMask
    {
        public float BaseValue { get; set; }
        public float BaseMultiplier { get; set; } = 1.0f;
        public float MaskMultiplier { get; set; }
    }
}
