using Assets.Scripts.Entities.Util.Config.Input;
using Assets.Scripts.Entities.Util.Config.Menu;
using Assets.Scripts.Sound;
using Assets.Scripts.Sound.Soundtrack;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private AudioClip _mainMenuMusic;
        private void Start()
        {
            SoundCompositeRoot.Instance.SoundTrackPlayer.Play(_mainMenuMusic);
        }
        private int selectedID = -1;
        public void SceneID(int id)
        {
            selectedID = id;    }

        public void LoadScene()
        {
            if (selectedID >= 0)
                SceneManager.LoadScene(selectedID);
        }
    }
}
