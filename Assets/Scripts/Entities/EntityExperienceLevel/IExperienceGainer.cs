using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.EntityExperienceLevel
{
    internal interface IExperienceGainer
    {
        int OnDieExp { get; }
    }
}
