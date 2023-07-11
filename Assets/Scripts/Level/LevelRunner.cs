using Assets.Scripts.CompositeRoots;
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
            _runHandlers = LevelCompositeRoot.Instance.LevelInfo.StartEntities.Select(x => x.GetComponent<ILevelRunHandler>()).Where(x => x != null);
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
