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
        public float RemainTime { get; set; }
        public IDurationable Mod { get; private set; }
        internal DurationableModifier(IDurationable mod)
        {
            Mod = mod;
            RemainTime = mod.Duration;
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
            bool modified = mod.ModifyStats();
            if(mod is IDurationable durationable && modified)
            {
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
                if (mod.RemainTime <= 0) OnModExpired(i);
                else
                {
                    mod.RemainTime -= Time.deltaTime;
                    _activeMods[i] = mod;
                }
                
            }
        }
        private void OnModExpired(int index)
        {
            _activeMods[index].Mod.UnmodifyStats();
            _activeMods.RemoveAt(index);
        }
    }
}
