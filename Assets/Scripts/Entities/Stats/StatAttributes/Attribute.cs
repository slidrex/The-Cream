using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatAttributes
{
    internal class Attribute : IAttribute
    {
        public Attribute(int baseValue)
        {
            BaseValue = baseValue;
            MultiplierMask = 1.0f;
            BaseMultiplier = 1.0f;
        }
        public void Modify(AttributeMask mask)
        {
            MultiplierMask += mask.MaskMultiplier;
            BaseMultiplier += mask.BaseMultiplier;
            BaseValue += mask.BaseValue;
        }
        public void Unmodify(AttributeMask mask)
        {
            MultiplierMask -= mask.MaskMultiplier;
            BaseMultiplier -= mask.BaseMultiplier;
            BaseValue -= mask.BaseValue;
        }
        public float MultiplierMask { get; private set; }
        public float BaseMultiplier { get; private set; }

        public int BaseValue { get; private set; }

        public int GetValue()
        {
            return (int)(BaseValue * BaseMultiplier * MultiplierMask);
        }
    }
}
