using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Level
{
    internal class LevelRunner : MonoBehaviour
    {
        private readonly Action<bool> OnLevelRun;
        private IEnumerable<ILevelRunHandler> _runHandlers;
        public bool IsLevelRunning { get; private set; }
        internal void Configure()
        {
            _runHandlers = LevelCompositeRoot.Instance.LevelInfo.StartEntities.SelectMany(x => x.GetComponents<ILevelRunHandler>()).NotNull();
        }
        public void RunLevel()
        {
            IsLevelRunning = true;
            foreach (var obj in _runHandlers)
            {
                obj.OnLevelRun(true);
            }

            OnLevelRun?.Invoke(true);
        }
        public void StopLevel()
        {
            IsLevelRunning = false;
            foreach (var obj in _runHandlers)
            {
                obj.OnLevelRun(false);
            }
            OnLevelRun?.Invoke(false);
        }
    }
}
