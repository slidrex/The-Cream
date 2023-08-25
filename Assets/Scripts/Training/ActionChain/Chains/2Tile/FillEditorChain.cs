using Assets.Scripts.Entities.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class FillEditorChain : TrainingActionChain
	{
		[SerializeField] private Color32 _highlightSpaceColor;
		[UnityEngine.SerializeField] private TextMeshProUGUI _space;
		[UnityEngine.SerializeField] private Transform _editorEntityContainer;
		protected override void OnConfigure(Player player)
		{
			Editor.Editor.Instance._spaceController.OnSpaceChanged += OnSpaceChanged;
		}
		private void OnSpaceChanged(int space)
		{
			if (Editor.Editor.Instance._spaceController.GetMaxSpaceReqiured() == space) OnEditorFilled();
		}
		private void OnEditorFilled()
		{
			Editor.Editor.Instance._spaceController.OnSpaceChanged -= OnSpaceChanged;
			ConfirmChain();
		}
	}
}
