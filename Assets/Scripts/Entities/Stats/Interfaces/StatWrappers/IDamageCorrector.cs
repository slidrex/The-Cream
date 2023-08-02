using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Interfaces
{
    public enum AdjustmentOperation
    {
        ADD,
        MULTIPLY
    }
    public struct AdjustmentMask
    {
        public AdjustmentOperation Operation;
        public int Value;

        public AdjustmentMask(AdjustmentOperation operation, int value)
        {
            Operation = operation;
            Value = value;
        }
    }
    internal interface IDamageCorrector
    {
        List<AdjustmentMask> Masks { set; get; }
        Action<int> OnDamageIncomed { set; get; }
    }
}
