using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Items.Collectable
{
	[RequireComponent(typeof(Collider2D))]
	internal abstract class CollectableItem : MonoBehaviour
	{
		[SerializeField] private bool _destroyOnCollect;
		protected Collider2D Collider { get; private set; }
		private void Awake()
		{
			Collider = GetComponent<Collider2D>();
			if (!Collider.isTrigger) throw new Exception("Collider of collectable item is not Trigger");
		}
		protected abstract EntityTypeBase CollectEntityTypes { get; }
		protected abstract void OnCollect(Entity entity);
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.TryGetComponent<Entity>(out var entity))
			{
				if (CollectEntityTypes.MatchesEntityType(entity))
				{
					OnCollect(entity);
					if (_destroyOnCollect) Destroy(gameObject);
				}
			}
		}
	}
}
