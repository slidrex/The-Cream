using Assets.Scripts.GameProgress;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu.Localization
{
	internal class LocalizationSwitcher : MonoBehaviour
	{
		[SerializeField] private Button _flagButton;
		[SerializeField] private Image _flagImage;
		[SerializeField] private Language[] _availableLanguages;
		[Serializable]
		public struct Language
		{
			public Sprite CountryFlag;
			public string LanguageCode;
		}
		private void Start()
		{
			_flagButton.onClick.AddListener(SwitchLanguage);
			StartCoroutine(InitApply());
		}
		private IEnumerator InitApply()
		{
			LocalizationSettings.InitializationOperation.WaitForCompletion();
			ApplySelectedLanguage();
			yield return null;
		}
		private void SwitchLanguage()
		{
			Analytics.CustomEvent("language_switched");
			PersistentData.SelectedLanguageIndex = (int)Mathf.Repeat(PersistentData.SelectedLanguageIndex + 1, _availableLanguages.Length);
			ApplySelectedLanguage();
		}
		private void ApplySelectedLanguage()
		{
			var lang = _availableLanguages[PersistentData.SelectedLanguageIndex];
			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(new UnityEngine.Localization.LocaleIdentifier(lang.LanguageCode));
			_flagImage.sprite = lang.CountryFlag;
		}
	}
}
