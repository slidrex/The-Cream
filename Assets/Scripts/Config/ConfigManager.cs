using Assets.Scripts.Config.Advanced;
using Assets.Scripts.Config.Audio;
using Assets.Scripts.Entities.Util.Config.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Config
{
    internal class ConfigManager
    {
        public InputConfig InputConfig { get; set; }
        public AudioConfig AudioConfig { get; set; }
        public AdvancedSettingsConfig SettingsConfig { get; set; }
        public static ConfigManager Instance { get; private set; }
        public ConfigManager()
        {
            if (Instance != null) return;
            Instance = this;
            ConfigureServices();
            LoadConfig();
        }
        private void ConfigureServices()
        {
            InputConfig = new();
            SettingsConfig = new();
            AudioConfig = new();
        }
        private void LoadConfig()
        {

        }
    }
}
