using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Brain
{
    /// <summary>
    /// Base class for Entity AI.
    /// </summary>
    internal abstract class EntityBrain<EntityType> : MonoBehaviour, IResettable where EntityType : Entity
    {
        protected EntityType Entity { get; private set; }
        protected void Awake()
        {
            Entity = GetComponent<EntityType>();
        }
		private void Update()
        {
            if (LevelCompositeRoot.Instance.Runner.IsLevelRunning)
            {
                RuntimeUpdate();
            }
        }
		private void FixedUpdate()
		{
			if (LevelCompositeRoot.Instance.Runner.IsLevelRunning)
			{
				RuntimeFixedUpdate();
			}
		}
		protected virtual void RuntimeUpdate()
        {

        }
        protected virtual void RuntimeFixedUpdate()
        {

        }

		public virtual void OnReset()
		{

		}
	}
}
