using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatAttributes
{
    internal struct AttributeMask
    {
        public int BaseValue { get; set; }
        public int BaseMultiplier { get; set; }
        public float MaskMultiplier { get; set; }
    }
}
