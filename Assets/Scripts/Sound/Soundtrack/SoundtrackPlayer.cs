using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Sound.Soundtrack
{
    internal class SoundtrackPlayer : MonoBehaviour, ISoundtrackPlayer
    {
        [SerializeField] private AudioSource _source;
        public static SoundtrackPlayer Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
        public void Play(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }

        public void Stop()
        {
            _source.Stop();
        }
    }
}
