using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Level
{
    internal class LevelBootstrapper : MonoBehaviour
    {
        public Action OnGameBegin, OnGameEnd;
        public void StartGame()
        {
            OnGameBegin?.Invoke();
        }
        public void EndGame()
        {
            OnGameEnd?.Invoke();
        }
    }
}
