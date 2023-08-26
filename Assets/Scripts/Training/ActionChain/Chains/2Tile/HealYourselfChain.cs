using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Characters;
using Assets.Scripts.Entities.Player.Skills;
using Assets.Scripts.LevelEditor.RuntimeSpace.Player;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class HealYourselfChain : TrainingActionChain
	{
		[UnityEngine.SerializeField] private GameObject _runtimeEntities;
		private Player _player;
		private void Awake()
		{
			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnModeChanged;
			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += AutoRun;
		}
		private void AutoRun(GameMode mode)
		{
			_runtimeEntities.gameObject.SetActive(mode == GameMode.RUNTIME);

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
			Editor.Editor.Instance._runtimeSystem.CastThroughGameObjects = true;
			_player =player;
			SetButtonInteractions(true);
			TrainingCompositeRoot.Instance.HighlightController.HighlightElements(false, _runtimeEntities);
			TrainingCompositeRoot.Instance.HighlightController.HighlightEntities(new List<SpriteRenderer>() { player.SpriteRenderer });
			player.OnHealthChanged += OnHealthChanged;
		}
		private void OnHealthChanged(int oldHealth, int newHealth, Entity dealer)
		{
			_player.OnHealthChanged -= OnHealthChanged;
			if (newHealth > oldHealth)
			{
				StageController.Singleton.DisableAutoactivateWave = false;
				TrainingCompositeRoot.Instance.HighlightController.UnhighlightElements();
				TrainingCompositeRoot.Instance.HighlightController.UnhighlightEntities();
				Editor.Editor.Instance._runtimeSystem.CastThroughGameObjects = false;
				LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnModeChanged;
				ConfirmChain();
			}
		}
	}
}
