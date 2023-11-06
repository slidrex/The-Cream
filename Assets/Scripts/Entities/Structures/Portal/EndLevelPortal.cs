using Assets.Scripts.CompositeRoots;
using System;
using UnityEngine;

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
