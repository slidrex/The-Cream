using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Characters;
using Assets.Scripts.Entities.Player.Skills.Implementations.Knight;
using Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class UseDoubleDamageChain : TrainingActionChain
	{
		[SerializeField] private GameObject _playerAbilities;
		private ParticleGenerator _particleGenerator;
		protected override void OnConfigure(Player player)
		{
			Time.timeScale = 0;

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
			ConfirmChain();
		}
	}
}
