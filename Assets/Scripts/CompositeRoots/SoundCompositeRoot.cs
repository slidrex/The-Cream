using Assets.Scripts.Sound.SoundEffect;
using Assets.Scripts.Sound.Soundtrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    internal class SoundCompositeRoot : MonoBehaviour
    {
        [field: SerializeField] public SoundEffectPlayer SoundPlayer { get; private set; }
        [field: SerializeField] public SoundtrackPlayer SoundTrackPlayer { get; private set; }
        [field: SerializeField] public SoundEffectStorage SoundEffectStorage { get; private set; }
        public static SoundCompositeRoot Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
