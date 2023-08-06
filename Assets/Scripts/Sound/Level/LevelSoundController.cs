using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Sound.Soundtrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Sound.Level
{
    internal class LevelSoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip _defaultRuntimeTheme;
        public AudioClip RuntimeTheme { get; set; }
        private void Awake()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelRun += OnRuntimeRun;
        }
        private void OnRuntimeRun(bool isTrue)
        {
            if (isTrue)
            {
                SoundtrackPlayer.Instance.Play(RuntimeTheme == null ? _defaultRuntimeTheme : RuntimeTheme);
            }
        }
    }
}
