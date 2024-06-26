﻿using Assets.Scripts.Functions;
using Assets.Scripts.Level.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stage.Interfaces
{
    internal interface IStageController
    {
        void StartStageLevel(int level, StageTileElementHolder holder, bool init = false);
        void Move(Direction direction);
    }
}
