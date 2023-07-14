using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Interfaces;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Stats
{
    internal struct DurationableModifier
    {
        public float _remainTime { get; private set; }
        private float _callInterval;
        private float _timeSinceTickCall;
        private IDurationable _durationable;
        private ITickable _tickable;
        private bool _isTickable;
        internal DurationableModifier(IDurationable mod)
        {
            _timeSinceTickCall = 0;
            _callInterval = 0;
            _isTickable = false;
            _durationable = mod;
            _tickable = null;
            _remainTime = mod.Duration;
        }
        internal DurationableModifier(ITickable mod, bool tickable)
        {
            _timeSinceTickCall = 0;
            _callInterval = mod.CallInterval;
            _isTickable = true;
            _durationable = mod;
            _tickable = mod;
            _remainTime = mod.Duration;
        }
        public bool ShouldDelete()
        {
            if (_isTickable) HandleTick();

            _remainTime -= Time.deltaTime;
            if (_remainTime <= 0)
            {
                _durationable.OnEffectEnd();
                return true;
            }
            return false;
        }
        private void HandleTick()
        {
            if (_timeSinceTickCall < _callInterval) _timeSinceTickCall += Time.deltaTime;
            else
            {
                _timeSinceTickCall = 0;
                _tickable.OnTick();
            }
        }
    }
    internal sealed class StatModifierHandler : MonoBehaviour, ILevelRunHandler
    {
        private List<DurationableModifier> _activeMods;
        private bool isRunning;
        public StatModifierHandler()
        {
            _activeMods = new();
        }
        public bool AddModifier(EntityStatModifier mod)
        {
            bool modified = mod.OnEffectStart();
            if(modified)
            {
                if(mod is ITickable tickable)
                    _activeMods.Add(new DurationableModifier(tickable, true));
                else if(mod is IDurationable durationable)
                    _activeMods.Add(new DurationableModifier(durationable));
            }

            return modified;
        }

        public void OnLevelRun(bool run)
        {
            isRunning = run;
        }
        private void Update()
        {
            if (isRunning) OnLevelUpdate();
        }
        private void OnLevelUpdate()
        {
            for(int i = 0; i <  _activeMods.Count; i++)
            {
                var mod = _activeMods[i];
                if (mod.ShouldDelete()) OnModExpired(i);
                else
                {
                    _activeMods[i] = mod;
                }
                
            }
        }
        private void OnModExpired(int index)
        {
            _activeMods.RemoveAt(index);
        }
    }
}
