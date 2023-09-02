using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Sound.Entity
{
	internal class PlayerSoundProducer : MonoBehaviour
	{
		[SerializeField] private AudioClip _stepSound;
		[SerializeField] private AudioClip _attackSound;
		public void PlayFootStepSound()
		{
			SoundCompositeRoot.Instance.SoundPlayer.Play(transform.position, _stepSound);
		}
		public void PlayAttackSound()
		{
			SoundCompositeRoot.Instance.SoundPlayer.Play(transform.position, _attackSound);
		}
	}
}
