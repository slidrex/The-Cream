using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Reset;
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
        public Action<bool> OnLevelRun;
        private IEnumerable<ILevelRunHandler> _runHandlers;
        private IEnumerable<IResettable> _resetHandlers;
        public bool IsLevelRunning { get; private set; }
        internal void Configure()
        {
            _resetHandlers = LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities.SelectMany(x => x.GetComponents<IResettable>()).NotNull();
            _runHandlers = LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities.SelectMany(x => x.GetComponents<ILevelRunHandler>()).NotNull();
        }
        public void RunLevel()
        {
            if (IsLevelRunning) throw new Exception("Level already run!");
            IsLevelRunning = true;
            TriggerResets(true);
            foreach (var obj in _runHandlers)
            {
                obj.OnLevelRun(true);
            }

            OnLevelRun?.Invoke(true);
        }
        public void StopLevel(bool ignoreTriggers = false)
        {
            if (!IsLevelRunning) throw new Exception("Level hasn't been started!");
            IsLevelRunning = false;
            foreach (var obj in _runHandlers)
            {
                obj.OnLevelRun(false);
            }
            OnLevelRun?.Invoke(false);
            if(ignoreTriggers == false)
                TriggerResets(false);
        }
        private void TriggerResets(bool before)
        {
            if(before)
                foreach(var obj in _resetHandlers)
                {
                    obj.OnReset();
                }

            foreach(var entity in LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities)
            {
                if (before)
                {
                    entity.OnBeforeReset();
                }
                else entity.OnAfterReset();
            }
        }
    }
}
