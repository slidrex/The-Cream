using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Config.Audio
{
    internal class AudioConfig
    {
        public Action OnChange { get; set; } 
        public float MasterVolume { get; private set; } = 1.0f;
        public float MusicVolumne { get; private set; } = 0.8f;
        public float SoundEffectsVolume { get; private set; } = 1.0f;
        public bool MusicMuted { get; private set; } = false;
        public bool SoundMuted { get; private set; } = false;
        public void SetMasterVolume(float value)
        {
            MasterVolume = value;
            OnChange?.Invoke();
        }
        public void SetMusicVolume(float value) 
        {
            MusicVolumne = value;
            OnChange?.Invoke();
        }
        public void SetSoundEffectsVolumne(float value)
        {
            SoundEffectsVolume = value;
            OnChange?.Invoke();
        }
        public void MuteMusic(bool mute)
        {
            MusicMuted = mute;
            OnChange?.Invoke();
        }
        public void MuteSoundEffects(bool mute)
        {
            SoundMuted = mute;
            OnChange?.Invoke();
        }
    }
}
