using Assets.Editor;
using Assets.Scripts.Stage;
using Assets.Scripts.Training.ActionChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Training
{
	internal class TrainingEntryPoint : MonoBehaviour
	{
		[SerializeField] private ChainsHolder _holder;
		private void OnEnable()
		{
			StageController.Singleton.OnAfterStageStarted += OnAfterStageStarted;
		}
		private void OnDisable()
		{
			StageController.Singleton.OnAfterStageStarted -= OnAfterStageStarted;
		}
		private void OnAfterStageStarted()
		{
			Editor.Editor.Instance._levelActions.ActivateButton(ButtonType.NONE);
		}
	}
}
