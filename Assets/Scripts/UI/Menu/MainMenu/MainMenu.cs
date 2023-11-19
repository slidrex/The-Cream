using Assets.Scripts.Sound;
using System.Collections;
using Assets.Scripts.PlatformConfig;
using Assets.Scripts.UI.Menu.Localization;
using UnityEngine;
using GameProgress;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private AudioClip _mainMenuMusic;
        [SerializeField] private GameObject loadScreen;
        [SerializeField] private FadingScreen fadingScreen;
        private const float delay = 0.5f;

        private void Start()
        {
            Yandex.LoadExternD();
        }

        private bool isInitialized;
        private void Pidoras()
        {
            SoundCompositeRoot.Instance.SoundTrackPlayer.Play(_mainMenuMusic);
            StartCoroutine(FindObjectOfType<LocalizationSwitcher>().StartEnum());
            
        }

        private void Update()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (isInitialized == false && PersistentData.IsLoaded)
            {
                isInitialized = true;
                Pidoras();
            }
#endif
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
                yield return new WaitForSeconds(delay);
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
