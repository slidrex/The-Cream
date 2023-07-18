using Assets.Scripts.Stage;
using Assets.Scripts.Stage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LevelEntry
{
    internal class LevelEntryPoint : MonoBehaviour
    {
        private StageController _stageController;
        private StageTileElementHolder[] _internalLevels;
        private int _currentStageLevel;
        private void Start()
        {
            ConfigureServices();
            StartNextStageLevel();
            _stageController.OnLastStageLeft += StartNextStageLevel;
        }
        private void OnDestroy()
        {
            _stageController.OnLastStageLeft -= StartNextStageLevel;
            
        }
        private void ConfigureServices()
        {
            _stageController = FindObjectOfType<StageController>();
            _internalLevels = GetComponentsInChildren<StageTileElementHolder>(true);
        }
        public void StartNextStageLevel()
        {
            if(_currentStageLevel > 0) _internalLevels[_currentStageLevel - 1].gameObject.SetActive(false);
            if (_currentStageLevel >= _internalLevels.Length)
            {
                OnLevelCompletelyFinished();
                return;
            }
            _internalLevels[_currentStageLevel].gameObject.SetActive(true);

            _stageController.StartStageLevel(_internalLevels[_currentStageLevel], _currentStageLevel == 0);

            _currentStageLevel++;
        }
        private void OnLevelCompletelyFinished()
        {
            print("ALL STAGED COMPLETELY FINISHED!");
        }
    }
}
