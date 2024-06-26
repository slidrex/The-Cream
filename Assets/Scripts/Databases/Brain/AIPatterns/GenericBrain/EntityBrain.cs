﻿using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Reset;
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
            if (LevelCompositeRoot.Instance.Runner.CurrentMode == Editor.GameMode.RUNTIME)
            {
                RuntimeUpdate();
            }
            OnUpdate();
        }
		private void FixedUpdate()
		{
			if (LevelCompositeRoot.Instance.Runner.CurrentMode == Editor.GameMode.RUNTIME)
			{
				RuntimeFixedUpdate();
			}
		}
        protected virtual void OnUpdate() { }
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
