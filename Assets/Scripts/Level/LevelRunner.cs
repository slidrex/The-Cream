using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Level
{
    internal class LevelRunner : MonoBehaviour
    {
        public Action<GameMode> OnLevelModeChanged { get; set; }
        public Editor.GameMode PreviousGameMode { get; private set; }
        private IEnumerable<ILevelRunHandler> _runHandlers;
        private IEnumerable<IResettable> _resetHandlers;
        public GameMode CurrentMode { get; private set; } = GameMode.UNASSIGNED;
        internal void Configure()
        {
            _resetHandlers = LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities.SelectMany(x => x.GetComponents<IResettable>()).Where(x => x != null);
            _runHandlers = LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities.SelectMany(x => x.GetComponents<ILevelRunHandler>()).Where(x => x != null);
        }
        public void SetGameMode(GameMode mode)
        {
            if (mode == CurrentMode && mode != GameMode.UNASSIGNED) throw new Exception($"Level mode is already set to {mode}!!");
            PreviousGameMode = CurrentMode;
            CurrentMode = mode;

            OnLevelModeChanged.Invoke(mode);
            foreach (var obj in _runHandlers)
            {
                obj.OnLevelRun(mode == GameMode.RUNTIME);
            }

        }
        public void TriggerResettableIterfaces()
        {
            foreach (var trigger in _resetHandlers) trigger.OnReset();
        }
    }
}
