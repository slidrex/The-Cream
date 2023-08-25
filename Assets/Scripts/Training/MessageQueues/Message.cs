using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Training.MessageQueues
{
	[CreateAssetMenu(menuName = "Cream/Training/Message")]
	internal class Message : ScriptableObject
	{
		[SerializeField] private string[] _messages;
		public string[] GetMessages() => _messages;
	}
}
