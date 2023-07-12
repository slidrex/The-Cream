using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Navigation.Interfaces
{
    internal interface IPullable
    {
        void Pull(Transform pullZone);
    }
}
