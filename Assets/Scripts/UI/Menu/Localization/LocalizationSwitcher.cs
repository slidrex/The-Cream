using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu.Localization
{
	internal class LocalizationSwitcher : MonoBehaviour
	{
		[SerializeField] private Button _flagButton;
		[SerializeField] private Image _flagImage;
		[SerializeField] private Language[] _availableLanguages;
		private int _selectedLanguage;
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
			_selectedLanguage = (int)Mathf.Repeat(_selectedLanguage + 1, _availableLanguages.Length);
			ApplySelectedLanguage();
		}
		private void ApplySelectedLanguage()
		{
			var lang = _availableLanguages[_selectedLanguage];
			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(new UnityEngine.Localization.LocaleIdentifier(lang.LanguageCode));
			_flagImage.sprite = lang.CountryFlag;
		}
	}
}
