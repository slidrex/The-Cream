using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.Advanced
{
	internal class NamingList : MonoBehaviour
	{
		[Serializable]
		public class ListObject
		{
			public ListIndex[] Containers;
			public int StartsWith = 1;
		}
		[SerializeField] private ListObject[] _lists;
		private void Awake()
		{
			foreach(var list in _lists)
			{
				for(int i = 0; i < list.Containers.Length; i++)
				{
					list.Containers[i].Index = list.StartsWith + i;
				}
			}
		}
	}
}
