using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatAttributes
{
    internal interface IAttribute
    {
        float MultiplierMask { get; }
        float BaseMultiplier { get; }
        int BaseValue { get; }
        int GetValue();
    }
}
