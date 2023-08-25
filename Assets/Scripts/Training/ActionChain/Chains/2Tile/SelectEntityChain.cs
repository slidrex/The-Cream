using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Training.ActionChain.Chains
{
	internal class SelectEntityChain : TrainingActionChain
	{
		[SerializeField] private GameObject _editorPlace;
		[SerializeField] private GameObject _spaceObject;
		[SerializeField] private TextMeshProUGUI _spaceText;
		protected override void OnConfigure(Player player)
		{
			HighlightElements(true);
			TrainingCompositeRoot.Instance.HighlightController.HighlightElements(false, _editorPlace, _spaceObject);
			Editor.Editor.Instance._editSystem.OnEntityPlaced += OnPlace;
		}
		private void OnPlace()
		{
			HighlightElements(false);
			TrainingCompositeRoot.Instance.HighlightController.UnhighlightElements();
			ConfirmChain();
			Editor.Editor.Instance._editSystem.OnEntityPlaced -= OnPlace;
		}
		private void HighlightElements(bool active)
		{
			var color = active ? Color.yellow : Color.white;
			_spaceText.color = color;
			EditorEntityHolder.DefaultSpawnColor = color;
			foreach (var obj in _editorPlace.GetComponentsInChildren<EditorEntityHolder>())
			{
				obj.SetCostColor(color);
			}
		}
	}
}
