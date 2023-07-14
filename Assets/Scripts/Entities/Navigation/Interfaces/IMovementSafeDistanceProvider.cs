﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Navigation.Interfaces
{
    internal interface IMovementSafeDistanceProvider
    {
        float SafeDistance { get; }
    }
}
