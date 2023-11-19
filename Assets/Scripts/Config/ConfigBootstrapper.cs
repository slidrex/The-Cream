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
