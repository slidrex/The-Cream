using Assets.Scripts.Sound;
using Assets.Scripts.Sound.Soundtrack;
using UnityEngine;

namespace EventStates
{
    public class PauseController
    {
        public static void PauseGame()
        {
            SoundCompositeRoot.Instance.SoundTrackPlayer.Pause();
            Time.timeScale = 0;
        }

        public static void UnpauseGame()
        {
            SoundCompositeRoot.Instance.SoundTrackPlayer.Unpause();
            Time.timeScale = 1;
        }
    }
}