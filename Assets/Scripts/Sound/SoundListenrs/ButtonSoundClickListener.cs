using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Sound.SoundListenrs
{
	internal class ButtonSoundClickListener : MonoBehaviour
	{
		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(PlayClickSound);
		}
		private void PlayClickSound()
		{
			SoundCompositeRoot.Instance.SoundPlayer.Play(Camera.main.transform.position, SoundCompositeRoot.Instance.SoundEffectStorage.ButtonClickSound);
		}
	}
}
