using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.Interfaces;
using Assets.Scripts.Entities.Stats.Structure.Aura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Structures.Structures.BuffStructures
{
    internal class HealingFountainAura : AuraStructure
    {
        protected override void OnActivate(Entity[] entitiesInRadius)
        {
        }
        protected override void OnAuraBecomeReady()
        {
            var allEntities = LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities;
            foreach(var entity in allEntities)
            {
                if(entity.TryGetComponent<IPullable>(out var comp))
                {
                    comp.Pull(transform);
                }
            }
            TryActivate();
        }
    }
}
