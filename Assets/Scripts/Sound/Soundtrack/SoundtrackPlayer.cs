using Assets.Scripts.Config;
using UnityEngine;

namespace Assets.Scripts.Sound.Soundtrack
{
    internal class SoundtrackPlayer : MonoBehaviour, ISoundtrackPlayer
    {
        [SerializeField] private AudioSource _source;
        private void OnEnable()
        {
            ConfigManager.Instance.AudioConfig.OnChange += UpdateSourceVolume;
        }
        private void OnDisable()
        {
            ConfigManager.Instance.AudioConfig.OnChange -= UpdateSourceVolume;
        }
        public void Play(AudioClip clip)
        {
            UpdateSourceVolume();

            _source.clip = clip;
            _source.Play();
        }

        public void Pause()
        {
            _source.Pause();
        }

        public void Unpause()
        {
            _source.UnPause();
        }
        private void UpdateSourceVolume()
        {
            _source.volume = ConfigManager.Instance.AudioConfig.MasterVolume * ConfigManager.Instance.AudioConfig.MusicVolumne * (ConfigManager.Instance.AudioConfig.MusicMuted ? 0 : 1);
        }

        public void Stop()
        {
            _source.Stop();
        }
    }
}
