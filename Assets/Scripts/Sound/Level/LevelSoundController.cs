using Assets.Editor;
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
        [SerializeField] private AudioClip _defaultNoneTheme;
        [SerializeField] private AudioClip _defaultRuntimeTheme;
        [SerializeField] private AudioClip _defaultEditorTheme;
        public AudioClip RuntimeTheme { get; private set; }
        public AudioClip EditorTheme { get; private set; }
        public bool ChangeStateThemes = true;
        private void Awake()
        {
            RuntimeTheme = _defaultRuntimeTheme;
            EditorTheme = _defaultEditorTheme;
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnRuntimeRun;
        }
        public void SetRuntimeTheme(AudioClip clip)
        {
            if (clip == null) RuntimeTheme = _defaultRuntimeTheme;
            else RuntimeTheme = clip;
        }
        private void OnRuntimeRun(GameMode mode)
        {
            if (ChangeStateThemes == false) return;
            switch (mode)
            {
                case GameMode.RUNTIME:
                    SoundCompositeRoot.Instance.SoundTrackPlayer.Play(RuntimeTheme); return;
                case GameMode.NONE:
                    SoundCompositeRoot.Instance.SoundTrackPlayer.Play(_defaultNoneTheme); return;
                case GameMode.EDIT:
                    SoundCompositeRoot.Instance.SoundTrackPlayer.Play(EditorTheme); return;
                case GameMode.UNASSIGNED:
                    SoundCompositeRoot.Instance.SoundTrackPlayer.Stop(); return;
            }
        }
    }
}
