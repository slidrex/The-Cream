using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Mobs.Loot
{
	[RequireComponent(typeof(Entity))]
	internal class LootTable : MonoBehaviour
	{
		[UnityEngine.SerializeField] private Item[] _items;
		[Serializable]
		public struct Item
		{
			[Range(0, 1f)] public float _dropChance;
			public GameObject _object;
		}
		public void DropLoot()
		{
			foreach(var item in _items)
			{
				float rand = UnityEngine.Random.Range(0f, 1f);

                if (rand < item._dropChance)
				{
					print(rand);
					Instantiate(item._object, transform.position, Quaternion.identity);
				}
			}
		}
	}
}
