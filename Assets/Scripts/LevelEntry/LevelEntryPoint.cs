﻿using Assets.Scripts.Databases.Database_providers;
using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.Databases.Model.Player;
using Assets.Scripts.Entities.Strategies;
using Assets.Scripts.GameProgress;
using Assets.Scripts.LevelEditor.RuntimeSpace.Player;
using Assets.Scripts.Stage;
using Assets.Scripts.Stage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.LevelEntry
{
    internal class LevelEntryPoint : MonoBehaviour
    {
        private StageController _stageController;
        private StageTileElementHolder[] _internalLevels;
        private int _currentStageLevel;
        public Action<StageTileElementHolder> OnHolderActivate;
        private PlayerRuntimeSpace _playerSpace;
        private void Start()
        {
            ConfigureServices();
            InitData();
            StartNextStageLevel();
            _stageController.OnLastStageLeft += StartNextStageLevel;
        }
        private void OnDestroy()
        {
            _stageController.OnLastStageLeft -= StartNextStageLevel;
        }
        private void InitData()
        {
            
        }
        private void ConfigureServices()
        {
            _playerSpace = FindObjectOfType<PlayerRuntimeSpace>();
            _stageController = FindObjectOfType<StageController>();
            _internalLevels = GetComponentsInChildren<StageTileElementHolder>(true);
        }
        public void StartNextStageLevel()
        {
            EntityBaseStrategy.OnGameStart();
            if(_currentStageLevel > 0) _internalLevels[_currentStageLevel - 1].gameObject.SetActive(false);
            if (_currentStageLevel >= _internalLevels.Length)
            {
                OnLevelCompletelyFinished();
                return;
            }
            _internalLevels[_currentStageLevel].gameObject.SetActive(true);
            _internalLevels[_currentStageLevel].Configure();
            _stageController.StartStageLevel(_internalLevels[_currentStageLevel], _currentStageLevel == 0);

            OnHolderActivate.Invoke(_internalLevels[_currentStageLevel]);
            _currentStageLevel++;
        }
        private void OnLevelCompletelyFinished()
        {
            SceneManager.LoadScene(0);
        }
    }
}
