using Assets.Scripts.Training.MessageQueues;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Training
{
	internal class MessageQueue : MonoBehaviour
	{
		[SerializeField] private Image _queueCanvas;
		[SerializeField] private TextMeshProUGUI _text;
		private Queue<string> _remainStrings;
		public void SetQueue(Message message)
		{
			_queueCanvas.gameObject.SetActive(true);
			_remainStrings = new Queue<string>(message.GetMessages());
		}
		public bool RenderNextString()
		{
			if(_remainStrings.TryDequeue(out var str))
			{
				_text.text = str;
				return true;
			}
			_remainStrings = null;
			_queueCanvas.gameObject.SetActive(false);
			return false;
		}
	}
}
