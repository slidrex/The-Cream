using Assets.Scripts.Entities.Util.Config.Input;
using Assets.Scripts.Entities.Util.Config.Menu;
using Assets.Scripts.Sound;
using Assets.Scripts.Sound.Soundtrack;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private AudioClip _mainMenuMusic;
        [SerializeField] private GameObject loadScreen;
        [SerializeField] private FadingScreen fadingScreen;
        private const float �������� = 0.5f;
        private void Start()
        {
            SoundCompositeRoot.Instance.SoundTrackPlayer.Play(_mainMenuMusic);
        }
        private int selectedID = -1;
        public void SceneID(int id)
        {
            selectedID = id;
        }

        public void LoadScene()
        {
            StartCoroutine(LoadSceneNumerator(selectedID));
        }
        public IEnumerator LoadSceneNumerator(int index)
        {
            if (index >= 0)
            {
                fadingScreen.FadeIn();
                yield return new WaitForSeconds(fadingScreen._fadeInLength);
                fadingScreen.FadeOut();
                loadScreen.SetActive(true);
                yield return new WaitForSeconds(��������);
                AsyncOperation loadScene = SceneManager.LoadSceneAsync(index);
                while (!loadScene.isDone)
                {
                    yield return null;
                }
                loadScreen.SetActive(false);
            }    
        }
    }
}
