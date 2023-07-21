﻿using Assets.Scripts.Level.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Stage
{
    internal class StageTileElement : MonoBehaviour
    {
        public Transform PlayerPosition;
        public int CameraSize = 5;
        public RelationElement[] Elements;
        [Serializable]
        public struct RelationElement
        {
            public Direction Direction;
            public StageTileElement Element;
        }
    }
}