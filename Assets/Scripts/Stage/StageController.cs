using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Navigation.Navigator;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Level.Stages;
using Assets.Scripts.Stage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Editor.Editor;

namespace Assets.Scripts.Stage
{
    internal class StageController : MonoBehaviour, IStageController
    {
        [SerializeField] private StageTileElement _initialElement;
        private Player _player;
        private Camera _camera;
        private StageTileElement _currentElement;
        private void Start()
        {
            Editor.Editor.Instance._spaceController.OnSpaceChanged = OnSpaceChanged;
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDie += OnEntityDie;
            _camera = Camera.main;
            StartGame();
        }
        private void OnEditorFullfilled()
        {
            Instance._levelActions.ActivateButton(ButtonType.START_RUNTIME);
        }
        private void OnEditorNotFullFilled()
        {
            Instance._levelActions.ActivateButton(ButtonType.NONE);
        }
        private void OnEntityDie(Entity entity)
        {
            UpdateRuntimeMap();
        }
        private void OnSpaceChanged(int newSpace)
        {
            bool isFullFilled = newSpace >= Editor.Editor.Instance._spaceController.GetMaxSpaceReqiured();
            if(isFullFilled) OnEditorFullfilled();
            else OnEditorNotFullFilled();
        }
        private void UpdateRuntimeMap()
        {
            var entities = NavigationUtil.GetEntitiesOfType(_player.TargetType, _player.transform);
            if(entities == null || entities.Count == 0 || entities.Any(x => x is IDamageable d && d.CurrentHealth > 0) == false) 
            {
                Instance._levelActions.ActivateButton(ButtonType.MOVE_NEXT_LEVEL);
            }
        }
        public void RestoreRuntime()
        {
            LevelCompositeRoot.Instance.Runner.RunLevel();
            Instance.SetGamemode(GameMode.RUNTIME);
        }
        public void StopLevel()
        {
            LevelCompositeRoot.Instance.Runner.StopLevel();
            Instance.SetGamemode(GameMode.EDIT);
        }
        private void OnButtonSwitched(ButtonType type)
        {
            if (type == ButtonType.MOVE_NEXT_LEVEL) EnableDockspace();
            else Instance.Dockspace.DisableDockspace();
        }
        public void Move(Direction direction)
        {
            var temp = _currentElement;

            Instance.Dockspace.DisableDockspace();
            for (int i = 0; i < temp.Elements.Length; i++) if (temp.Elements[i].Direction == direction) SetCurrentElement(temp.Elements[i].Element);

            Instance.SetGamemode(GameMode.EDIT);
            Instance._levelActions.ActivateButton(ButtonType.NONE);
        }

        public void StartGame()
        {
            _player = Instance.PlayerSpace.InitPlayer(_initialElement.PlayerPosition.transform.position);
            SetCurrentElement(_initialElement);
            Instance._levelActions.OnButtonSwitched = OnButtonSwitched;
            Instance.SetGamemode(GameMode.NONE);
            Instance._levelActions.ActivateButton(ButtonType.MOVE_NEXT_LEVEL);
        }
        private void SetCurrentElement(StageTileElement element)
        {
            _currentElement = element;
            _camera.transform.position = new Vector3(_currentElement.transform.position.x, _currentElement.transform.position.y, _camera.transform.position.z);
            _player.transform.position = element.PlayerPosition.position;
            _camera.orthographicSize = _currentElement.CameraSize;
        }
        private void EnableDockspace()
        {
            var availableDirections = _currentElement.Elements.Select(x => x.Direction).ToArray();
            Instance.Dockspace.EnableDockspace(availableDirections, this);
        }
    }
}
