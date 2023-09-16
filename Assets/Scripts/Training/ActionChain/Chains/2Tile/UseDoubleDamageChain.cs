using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Characters;
using Assets.Scripts.Entities.Player.Skills;
using Assets.Scripts.Entities.Player.Skills.Implementations.Knight;
using Assets.Scripts.LevelEditor.RuntimeSpace.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class UseDoubleDamageChain : TrainingActionChain
	{
		[SerializeField] private GameObject _playerAbilities;
		private ParticleGenerator _particleGenerator;
		private void Awake()
		{
			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnLevelChanged;
		}
		private void OnLevelChanged(GameMode mode)
		{
			if(mode == GameMode.RUNTIME)
			ChangeInteractions(false);
		}
		private void ChangeInteractions(bool active)
		{
			foreach(var item in _playerAbilities.GetComponentsInChildren<Button>())
			{
				item.interactable = active;
			}
			(FindObjectOfType<PlayerRuntimeSpace>().GetPlayerSkillModels()[0].Skill as PlayerActiveSkill<Knight>).DisableActivate = !active;
		}
		protected override void OnConfigure(Player player)
		{
			Time.timeScale = 0;
			ChangeInteractions(true);
			TrainingCompositeRoot.Instance.HighlightController.HighlightElements(false, _playerAbilities);
			TrainingCompositeRoot.Instance.HighlightController.HighlightEntities(new List<SpriteRenderer>() { player.SpriteRenderer });

			_particleGenerator = player.GetComponent<ParticleGenerator>();
			_particleGenerator.OnParticlesEnabled += OnDoubleDamageUsed;
		}
		private void OnDoubleDamageUsed()
		{
			Time.timeScale = 1;
			TrainingCompositeRoot.Instance.HighlightController.UnhighlightElements();
			TrainingCompositeRoot.Instance.HighlightController.UnhighlightEntities();
			_particleGenerator.OnParticlesEnabled -= OnDoubleDamageUsed;
			LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnLevelChanged;
			ConfirmChain();
		}
	}
}
