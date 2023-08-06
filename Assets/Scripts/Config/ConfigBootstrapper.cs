using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Config
{
    internal class ConfigBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            new ConfigManager();
        }
    }
}
