using Assets.Scripts.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Sound.Soundtrack
{
    internal class SoundEffectPlayer : MonoBehaviour
    {
		private void Start()
		{
			_listenerTransform = FindObjectOfType<AudioListener>().transform;
		}
		private void Update()
		{
			if(_listenerTransform == null) _listenerTransform = FindObjectOfType<AudioListener>()?.transform;
		}
		private Transform _listenerTransform;
        public void Play(AudioClip clip)
        {
            Play(_listenerTransform.position, clip);
		}
		public void Play(Vector3 position, AudioClip clip)
		{
			if(ConfigManager.Instance.AudioConfig.SoundMuted == false)
                AudioSource.PlayClipAtPoint(clip, new Vector3(_listenerTransform.position.x, position.y, position.z), ConfigManager.Instance.AudioConfig.SoundEffectsVolume * ConfigManager.Instance.AudioConfig.MasterVolume);
        }
    }
}
