﻿using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Level;
using Assets.Scripts.Level.InfoProviders;
using Assets.Scripts.Sound.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CompositeRoots
{
    internal class LevelCompositeRoot : MonoBehaviour
    {
        public static LevelCompositeRoot Instance { get; private set; }
        [field: SerializeField] public LevelSoundController LevelSoundController { get; private set; }
        public LevelRunner Runner;
        public LevelEntityInfo LevelInfo;
        private void Awake()
        {
            Instance = this;
            Configure();
        }
        private void Configure()
        {
            LevelInfo.ConfigureLevelInfo();
            Runner.Configure();
        }
    }
}
