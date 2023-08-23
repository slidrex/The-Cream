using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Databases.Database_providers;
using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.Databases.Model.Player;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Strategies;
using Assets.Scripts.Entities.Structures.Portal;
using Assets.Scripts.GameProgress;
using Assets.Scripts.Level;
using Assets.Scripts.Level.EntryPoint;
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
        private EndLevelPortal _portal;
        private StageController _stageController;
        private StageTileElementHolder[] _internalLevels;
        private int _currentStageLevel;
        public Action<StageTileElementHolder> OnHolderActivate;
        private void Start()
        {
            _portal = FindObjectOfType<EntryPointData>().Portal;
            ConfigureServices();
            InitData();
            StartNextStageLevel();



            _stageController.OnLastStageLeft += OnStageLevelOver;
            LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.NONE);
        }
        private void OnDestroy()
        {
            _stageController.OnLastStageLeft -= OnStageLevelOver;
        }
        private void InitData()
        {
            LevelCompositeRoot.Instance.BootStrapper.StartGame();
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
            _currentStageLevel++;
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
            print(_currentStageLevel + " " + _internalLevels.Length);
            portal.OnActivateAction = _currentStageLevel >= _internalLevels.Length ? EndLevelPortalAction : NextLevelPortalAction;
        }
        private void NextLevelPortalAction()
        {
            StartNextStageLevel();
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
