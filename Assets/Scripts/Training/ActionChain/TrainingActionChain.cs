using Assets.Scripts.Entities.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Training.ActionChain
{
	internal abstract class TrainingActionChain : MonoBehaviour
	{
		public string ActionDescriptionKey;
		public Action OnChainCompleted;
		protected ChainsHolder _holder;
		protected void MoveToPreviousChain()
		{
			_holder.MoveToPreviousChain();
		}
		public void StartListening(Player player, ChainsHolder holder, Action onChainCompleted)
		{
			OnConfigure(player);
			OnChainCompleted = onChainCompleted;
			_holder = holder;
		}
        protected abstract void OnConfigure(Player player);
		protected void ConfirmChain()
		{
			OnChainCompleted.Invoke();
			OnChainCompleted = null;
		}
	}
}
