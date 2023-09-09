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
		[DllImport("__Internal")]
		public static extern void ShowAdv();
		[DllImport("__Internal")]
		public static extern void RateGame();
		[DllImport("__Internal")]
		public static extern void Hello();
	}
}
