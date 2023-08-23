using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Level;
using Assets.Scripts.Level.InfoProviders;
using Assets.Scripts.Sound.Level;
using Assets.Scripts.UI.MiniMap;
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
        public MiniMapController MiniMap;
        [field: SerializeField] public LevelSoundController LevelSoundController { get; private set; }
        public LevelRunner Runner;
        public LevelBootstrapper BootStrapper;
        public LevelMetaInfo MetaInfo { get; set; }
        public LevelEntityInfo LevelInfo;
        private void Awake()
        {
            Instance = this;
            Configure();
        }
        private void OnDestroy()
        {
            Unconfigure();
        }
        private void Configure()
        {
            LevelInfo.ConfigureLevelInfo();
            Runner.Configure();
            MiniMap.Configure();
            MetaInfo = new();
        }
        private void Unconfigure()
        {
            MiniMap.Unconfigure();
        }
    }
}
