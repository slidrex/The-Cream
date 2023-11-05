using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatAttributes
{
    public class Stat : IAttribute
    {
        public Stat(float baseValue)
        {
            BaseValue = baseValue;
            MultiplierMask = 1.0f;
            BaseMultiplier = 1.0f;
        }
        public float MultiplierMask { get; internal set; }
        public float BaseMultiplier { get; internal set; }

        public float BaseValue { get; internal set; }

        public float GetValue() => BaseValue * BaseMultiplier * MultiplierMask;
    }
}
