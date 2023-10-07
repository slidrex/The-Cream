using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
		private static extern void Hello();
#endif
		public static Yandex Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
		}

		public void RateGameButton()
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
			RateGame();
			#endif
		}

		public void ShowAdv()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			ShowAdv();
#endif
		}

		public void HelloButton()
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
			Hello();
			#endif
		}
	}
}
