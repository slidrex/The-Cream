using Assets.Scripts.Config;
using UnityEngine;

namespace Assets.Scripts.Sound.Soundtrack
{
    internal class SoundEffectPlayer : MonoBehaviour
    {
		private Transform _listenerTransform;
		private void Start()
		{
			_listenerTransform = FindObjectOfType<AudioListener>().transform;
			print("start");
		}
		private void Update()
		{
			if(_listenerTransform == null) _listenerTransform = FindObjectOfType<AudioListener>().transform;
		}
        public void Play(AudioClip clip)
        {
            Play(_listenerTransform.position, clip);
		}
		public void Play(Vector3 position, AudioClip clip)
		{
			if(ConfigManager.Instance.AudioConfig.SoundMuted == false)
			{
                AudioSource.PlayClipAtPoint(clip, new Vector3(_listenerTransform.position.x, position.y, position.z), ConfigManager.Instance.AudioConfig.SoundEffectsVolume * ConfigManager.Instance.AudioConfig.MasterVolume);
			}
        }
    }
}
