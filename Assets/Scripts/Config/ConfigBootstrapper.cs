using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

namespace Assets.Scripts.Config
{
    internal class ConfigBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
			UnityServices.InitializeAsync();
			new ConfigManager();
        }
		private void Start()
		{
			AnalyticsService.Instance.StartDataCollection();
			
		}
	}
}
