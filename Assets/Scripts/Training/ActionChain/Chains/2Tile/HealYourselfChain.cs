using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Databases.dto;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class HealYourselfChain : TrainingActionChain
	{
		[SerializeField] private GameObject _runtimeEntities;
		private Player _player;
		private void Awake()
		{
			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnModeChanged;
		}
		private void OnModeChanged(GameMode mode)
		{
			SetButtonInteractions(false);
		}
		private void SetButtonInteractions(bool interactable)
		{
			foreach (var obj in _runtimeEntities.GetComponentsInChildren<Button>())
			{
				obj.interactable = interactable;
			}
			Editor.Editor.Instance._runtimeSystem.DisablePlacingEntities = !interactable;
		}
		protected override void OnConfigure(Player player)
		{
            _runtimeEntities.gameObject.SetActive(true);
			Editor.Editor.Instance._runtimeSystem.CastThroughGameObjects = true;
			_player = player;
			SetButtonInteractions(true);
			TrainingCompositeRoot.Instance.HighlightController.HighlightElements(false, _runtimeEntities);
			TrainingCompositeRoot.Instance.HighlightController.HighlightEntities(new List<SpriteRenderer>() { player.SpriteRenderer });
			Editor.Editor.Instance._runtimeSystem.OnEntityPlaced += OnEnitiyPlaced;
			Time.timeScale = 0;
		}
		private void OnEnitiyPlaced()
		{
			Time.timeScale = 1;
            Editor.Editor.Instance._runtimeSystem.OnEntityPlaced -= OnEnitiyPlaced;
			StageController.Singleton.DisableAutoactivateWave = false;
			TrainingCompositeRoot.Instance.HighlightController.UnhighlightElements();
			TrainingCompositeRoot.Instance.HighlightController.UnhighlightEntities();
			Editor.Editor.Instance._runtimeSystem.CastThroughGameObjects = false;
			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnModeChanged;
			ConfirmChain();
		}
	}
}
