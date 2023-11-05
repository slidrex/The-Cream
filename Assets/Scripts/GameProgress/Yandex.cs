using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GameProgress;
using UnityEngine;


namespace Assets.Scripts.PlatformConfig
{
	public class Yandex : MonoBehaviour
	{
	
#if UNITY_WEBGL && !UNITY_EDITOR
	[DllImport("__Internal")]
		private static extern void ShowAdv();

		[DllImport("__Internal")]
		private static extern void RateGame();
		[DllImport("__Internal")]
		private static extern void LoadExtern();
		[DllImport("__Internal")]
		private static extern void SaveExtern(string data);
#endif
		public static Yandex Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
			{
				transform.parent = null;
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public void RateGameButton()
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
			RateGame();
			#endif
		}

		public void ShowAdvert()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			ShowAdv();
#endif
		}
		public void SaveExternData()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			SaveExtern(PersistentData.GenerateData());
#endif
		}

		public static void LoadExternD()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			LoadExtern();
#endif
		}
		public void LoadData(string data)
		{
			PersistentData.LoadData(data);
		}
	}
}
