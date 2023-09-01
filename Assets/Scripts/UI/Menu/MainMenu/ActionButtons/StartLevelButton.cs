using Assets.Scripts.GameProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Menu.MainMenu.ActionButtons
{
	internal class StartLevelButton : MonoBehaviour
	{
		[SerializeField] private Transform _startLevel, _localization, _levels;
		[SerializeField] private GameObject _tutorialScreen;
		[SerializeField] private Menus.MainMenu _menu;
		[SerializeField] private GameObject _tutorialAlers;
		public bool ShowedByStartButton { get; set; }
		public void OnClick()
		{
			if (PersistentData.IsTutorialPassed)
			{
				StartGame();
			}
			else
			{
				ShowedByStartButton = true;
				ShowTutorialScreenFromStartButton(true);
			}
		}
		public void StartGame()
		{
			PersistentData.IsTutorialPassed = true;
			_startLevel.gameObject.SetActive(false);
			_levels.gameObject.SetActive(true);
			_localization.gameObject.SetActive(false);
		}
		public void StartTutorial()
		{
			StartCoroutine(_menu.LoadSceneNumerator(1));
		}
		public void ShowTutorialScreenFromStartButton(bool show)
		{
			ShowTutorialScreen(show);
		}
		public void ShowTutorialScreenFromOptions(bool show) 
		{
			ShowTutorialScreen(show);
		}
		public void ShowTutorialScreen(bool show)
		{
			_tutorialScreen.SetActive(show);
			
			if(show)
				Analytics.CustomEvent("training_started", new Dictionary<string, object> { ["showed_by_start_game"] = ShowedByStartButton });
			else if(show == false && ShowedByStartButton) Analytics.CustomEvent("training_rejected_showed_by_start_game");
			
			if (!PersistentData.IsTutorialPassed && ShowedByStartButton && show == false)
			{
				PersistentData.IsTutorialPassed = true;
				_tutorialAlers.SetActive(true);
				StartGame();
			}
		}
	}
}
