using Assets.Scripts.CompositeRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Brain
{
    [RequireComponent(typeof(Entity))]
    internal class EntityBrain : MonoBehaviour
    {
        protected Entity Entity { get; private set; }
        private void Awake()
        {
            Entity = GetComponent<Entity>();
        }
        private void Update()
        {
            if (LevelCompositeRoot.Instance.Runner.IsLevelRunning) RuntimeUpdate();       
        }
        protected virtual void RuntimeUpdate()
        {

        }
    }
}
