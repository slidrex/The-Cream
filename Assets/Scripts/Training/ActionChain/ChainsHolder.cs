using Assets.Scripts.Entities.Player;
using Assets.Scripts.LevelEntry;
using Assets.Scripts.Training.ActionChain.Chains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Assets.Scripts.Training.ActionChain
{
	internal class ChainsHolder : MonoBehaviour
	{
		[SerializeField] private LocalizeStringEvent _localizator; 
		[SerializeField] private TextMeshProUGUI _helpText;
		private Queue<TrainingActionChain> _chainsQueue;
		private Player _player;
		[SerializeField] private TrainingActionChain[] _chains;
		private TrainingActionChain _previousChain;
		private TrainingActionChain _currentChain;
		private bool _isHoldingItem;
		private void Awake()
		{
			Editor.Editor.Instance.OnAfterPlayerInitialized = Init;
		}
		private void Init()
		{
			ConfigureChains();
			MoveNextChain();
		}
		public void MoveToPreviousChain()
		{
			_isHoldingItem = true;
			InitChain(_previousChain);
		}
		private void ConfigureChains()
		{
			_player = FindObjectOfType<Player>();
			_chainsQueue = new Queue<TrainingActionChain>(_chains);
		}
		public void UpdateHelpText(string key)
		{
			_localizator.SetEntry(key);
			_localizator.RefreshString();
		}
		private void MoveNextChain()
		{
			if (_isHoldingItem == false)
			{
				_previousChain = _currentChain;
				_currentChain = _chainsQueue.Dequeue();
			}
			else
				_isHoldingItem = false;
			InitChain(_currentChain);
		}
		private void TryMoveNextChain()
		{
			if (_chainsQueue.Count > 0 || _isHoldingItem) MoveNextChain();
			else UpdateHelpText("Иди нахуй пидорас");
		}
		private void InitChain(TrainingActionChain chain)
		{
			chain.StartListening(_player, this, TryMoveNextChain);
			UpdateHelpText(chain.ActionDescriptionKey);
		}
	}
}
