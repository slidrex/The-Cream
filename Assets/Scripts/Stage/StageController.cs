using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Navigation.Navigator;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Stats.Interfaces.States;
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
        private Player _player;
        private Camera _camera;
        private StageTileElement _currentElement;
        private StageTileElement _endElement;
        public Action OnLastStageLeft;
        private StageTileElementHolder _currentStageLevel;
        private bool _runtimeActivated;
        private void OnEnable()
        {
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDamaged += OnEntityDamaged;
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDie += OnEntityDie;
            _camera = Camera.main;
            Editor.Editor.Instance._spaceController.OnSpaceChanged = OnSpaceChanged;
        }
        private void OnDisable()
        {
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDamaged -= OnEntityDamaged;
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDie -= OnEntityDie;
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
        public void UpdateRuntimeMap()
        {
            var entities = NavigationUtil.GetEntitiesOfType(_player.TargetType, _player.transform);
            if(entities == null || entities.Count == 0 || entities.Any(x => x is IDamageable d && d.CurrentHealth > 0) == false) 
            {
                if(_currentElement == _endElement)
                {
                    OnLastStageCompleted();
                }
                else
                {
                    Instance._levelActions.ActivateButton(ButtonType.MOVE_NEXT_LEVEL);
                    var ents = LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities;
                    for(int i = 0; i < ents.Count; i++)
                    {
                        if (ents[i] is IStatic == false)
                            Destroy(ents[i].gameObject);
                    }
                }
            }
        }
        private void OnLastStageCompleted()
        {
            OnLastStageLeft.Invoke();
        }
        public void RestoreRuntime()
        {
            LevelCompositeRoot.Instance.Runner.RunLevel();
            Instance.SetGamemode(GameMode.RUNTIME);
            Instance._levelActions.ActivateButton(ButtonType.STOP_RUNTIME);
            _runtimeActivated = true;
        }
        private void OnEntityDamaged()
        {
            if (Instance.CurrentGamemode == GameMode.RUNTIME && _runtimeActivated)
            {
                Instance._levelActions.ActivateButton(ButtonType.NONE);
                _runtimeActivated = false;
            }
        }
        public void StopLevel()
        {
            LevelCompositeRoot.Instance.Runner.StopLevel();
            Instance.SetGamemode(GameMode.EDIT);
            Instance._levelActions.ActivateButton(ButtonType.NONE);
            OnSpaceChanged(Instance._spaceController.CurrentSpaceReqiured);
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
        }
        private void InitPlayer()
        {
            _player = Instance.PlayerSpace.InitPlayer(_currentStageLevel.InitialElement.PlayerPosition.transform.position);
        }
        public void StartStageLevel(StageTileElementHolder currentStageLevel, bool init = false)
        {
            _currentStageLevel = currentStageLevel;
            if (init) InitPlayer();
            _endElement = currentStageLevel.EndElement;
            SetCurrentElement(currentStageLevel.InitialElement);
            Instance._levelActions.OnButtonSwitched = OnButtonSwitched;
            Instance.SetGamemode(GameMode.NONE);
            Instance._levelActions.ActivateButton(ButtonType.MOVE_NEXT_LEVEL);
        }
        private void SetCurrentElement(StageTileElement element)
        {
            _currentElement = element;
            _camera.transform.position = new Vector3(_currentElement.transform.position.x, _currentElement.transform.position.y, _camera.transform.position.z);
            _player.transform.position = _currentElement.PlayerPosition.position;
            _camera.orthographicSize = _currentElement.CameraSize;
        }
        private void EnableDockspace()
        {
            var availableDirections = _currentElement.Elements.Select(x => x.Direction).ToArray();
            Instance.Dockspace.EnableDockspace(availableDirections, this);
        }
    }
}
