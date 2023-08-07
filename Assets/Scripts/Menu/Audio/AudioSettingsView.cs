using Assets.Scripts.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Audio
{
    internal class AudioSettingsView : MonoBehaviour
    {
        [SerializeField] private Slider _master;
        [SerializeField] private Slider _soundEffect;
        [SerializeField] private Slider _music;
        [SerializeField] private Toggle _muteMusic;
        [SerializeField] private Toggle _muteSound;
        private void Start()
        {
            ConfigureInitialValues();
            ConfigureServices();
        }
        private void ConfigureServices()
        {
            _master.onValueChanged.AddListener((float val) => OnSoundChanged());
            _soundEffect.onValueChanged.AddListener((float val) => OnSoundChanged());
            _music.onValueChanged.AddListener((float val) => OnSoundChanged());
            _muteMusic.onValueChanged.AddListener((bool toggle) => OnMuteMusicToggled(toggle));
            _muteSound.onValueChanged.AddListener((bool toggle) => OnSoundToggled(toggle));
        }
        private void ConfigureInitialValues()
        {
            _muteMusic.SetIsOnWithoutNotify(ConfigManager.Instance.AudioConfig.MusicMuted);
            _muteSound.SetIsOnWithoutNotify(ConfigManager.Instance.AudioConfig.SoundMuted);
            _soundEffect.value = ConfigManager.Instance.AudioConfig.SoundEffectsVolume;
            _master.value = ConfigManager.Instance.AudioConfig.MasterVolume;
            _music.value = ConfigManager.Instance.AudioConfig.MusicVolumne;
        }
        private void OnSoundChanged()
        {
            ConfigManager.Instance.AudioConfig.SetMasterVolume(_master.value);
            ConfigManager.Instance.AudioConfig.SetMusicVolume(_music.value);
            ConfigManager.Instance.AudioConfig.SetSoundEffectsVolumne(_soundEffect.value);
        }
        private void OnMuteMusicToggled(bool mute)
        {
            ConfigManager.Instance.AudioConfig.MuteMusic(mute);
        }
        private void OnSoundToggled(bool mute)
        {
            ConfigManager.Instance.AudioConfig.MuteSoundEffects(mute);
        }
    }
}
