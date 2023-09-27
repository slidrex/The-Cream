using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.GameProgress;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Entities.Structures.Portal
{
	[UnityEngine.RequireComponent(typeof(BoxCollider2D))]
	internal class EndLevelPortal : BaseEntity, IPlayerSelectTrigger
    {
		[SerializeField] private bool _activeByCollision;
		private float rotationSpeed = 35;
		public Action OnActivateAction;
		private void Awake()
		{
			GetComponent<BoxCollider2D>().isTrigger = true;
		}
        private void Update()
        {
			transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - Time.deltaTime * rotationSpeed);
        }
        public void OnSelect()
        {
			if (_activeByCollision) return;
			Activate();

		}
		private void Activate()
		{
            LevelCompositeRoot.Instance.BootStrapper.EndGame();
            OnActivateAction.Invoke();
            Destroy(gameObject);
		}
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (_activeByCollision) Activate();
		}
	}
}
