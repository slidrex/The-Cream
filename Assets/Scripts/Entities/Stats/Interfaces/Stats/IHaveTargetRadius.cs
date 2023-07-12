using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Interfaces.Stats
{
    /// <summary>
    /// Entity keeps moving until target is inside TargetRadius
    /// </summary>
    internal interface IHaveTargetRadius
    {
        float TargetRadius { get; set; }
    }
}
