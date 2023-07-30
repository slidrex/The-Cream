using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Brain.Boss.Bossbar
{
    internal class EntityBossbar : MonoBehaviour, ILevelRunHandler
    {
        private BossbarController _barContorller;
        [field: SerializeField] public Color32 BarColor { get; private set; }
        private void Awake()
        {
            _barContorller = FindObjectOfType<BossbarController>();
        }

        public void OnLevelRun(bool run)
        {
            if (run)
            {
                _barContorller.EnableBar(this, GetComponent<Entity>());
            }
            else _barContorller.DisableBar();
        }
    }
}
