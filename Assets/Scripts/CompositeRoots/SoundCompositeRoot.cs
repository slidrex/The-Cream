using Assets.Scripts.Sound.SoundEffect;
using Assets.Scripts.Sound.Soundtrack;
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
            if(Instance == null)
                Instance = this;
            else
                Destroy(Instance);
        }
    }
}
