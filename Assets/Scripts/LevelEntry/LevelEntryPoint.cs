using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Strategies;
using Assets.Scripts.Entities.Structures.Portal;
using Assets.Scripts.GameProgress;
using Assets.Scripts.Level;
using Assets.Scripts.Level.EntryPoint;
using Assets.Scripts.Level.Interfaces;
using Assets.Scripts.LevelEditor.RuntimeSpace.Player;
using Assets.Scripts.Stage;
using Assets.Scripts.Stage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.LevelEntry
{
    internal class LevelEntryPoint : MonoBehaviour
    {
        private EndLevelPortal _portal;
        private StageController _stageController;
        private StageTileElementHolder[] _internalLevels;
        private int _currentStageLevel;
        public Action<StageTileElementHolder> OnHolderActivate;
        private IEnumerable<IStageBeginHandler> _stageBeginHandlers;
        private GameMode _startingGamemode = GameMode.NONE;
        private void Start()
        {
            ConfigureServices();
            InitData();
            StartNextStageLevel();



			_stageController.OnLastStageLeft += OnStageLevelOver;
            LevelCompositeRoot.Instance.Runner.SetGameMode(_startingGamemode);
        }
        private void ConfigureStaticEntities()
        {
            foreach(var e in FindObjectsOfType<Entity>())
            {
                e.IsStatic = true;
            }
        }
        private void OnDestroy()
        {
            _stageController.OnLastStageLeft -= OnStageLevelOver;
        }
        private void InitData()
        {
            _portal = FindObjectOfType<EntryPointData>().Portal;
            LevelCompositeRoot.Instance.BootStrapper.StartGame();
            var entities = FindObjectsOfType<GameObject>();
            _stageBeginHandlers = new List<IStageBeginHandler>();
            foreach (var e in entities)
            {
                if(e.TryGetComponent<IStageBeginHandler>(out var handler))
                {
                    _stageBeginHandlers.Append(handler);
                }
            }
        }
        private void ConfigureServices()
        {
            _stageController = FindObjectOfType<StageController>();
            _internalLevels = GetComponentsInChildren<StageTileElementHolder>(true);
        }
        private void StartNextStageLevel()
        {
            if(_currentStageLevel > 0)
                DisablePreviousStageLevel();
            _stageController.StartStageLevel(_currentStageLevel + 1, _internalLevels[_currentStageLevel], _currentStageLevel == 0);
            _internalLevels[_currentStageLevel].gameObject.SetActive(true);
            _internalLevels[_currentStageLevel].Configure();
            LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.UNASSIGNED);
            OnHolderActivate.Invoke(_internalLevels[_currentStageLevel]);
            EntityBaseStrategy.OnGameStart();
            ConfigureStaticEntities();

			NotifyHandlers();
            _currentStageLevel++;
        }
        private void NotifyHandlers()
        {
            foreach(var handler in _stageBeginHandlers)
            {
                handler.OnStageBegin();
            }
        }
        public void OnStageLevelOver()
        {
            OnStageEnd();

            if (_currentStageLevel >= _internalLevels.Length)
            {
                OnLastStagePassed();
            }
        }
        private void DisablePreviousStageLevel()
        {
            _internalLevels[_currentStageLevel - 1].gameObject.SetActive(false);
        }
        private void OnStageEnd()
        {
            var portal = Instantiate(_portal, FindObjectOfType<Player>().transform.position, Quaternion.identity);
            
            portal.OnActivateAction = _currentStageLevel >= _internalLevels.Length ? EndLevelPortalAction : StartNextStageLevel;
        }
        private void EndLevelPortalAction()
        {
            LevelProgressResolver.PassLevel(LevelMetaInfo.ActiveGameLevel);
            SceneManager.LoadScene(0);
        }
        private void OnLastStagePassed()
        {

        }
    }
}
