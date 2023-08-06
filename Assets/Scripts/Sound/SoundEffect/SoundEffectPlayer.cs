using Assets.Scripts.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Sound.Soundtrack
{
    internal class SoundEffectPlayer : MonoBehaviour
    {
        public void Play(Vector2 position, AudioClip clip)
        {
            if(ConfigManager.Instance.AudioConfig.SoundMuted == false)
                AudioSource.PlayClipAtPoint(clip, position, ConfigManager.Instance.AudioConfig.SoundEffectsVolume * ConfigManager.Instance.AudioConfig.MasterVolume);
        }
    }
}
