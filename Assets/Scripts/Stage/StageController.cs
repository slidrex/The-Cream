using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Stats.Interfaces.States;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Structures.Portal;
using Assets.Scripts.Functions;
using Assets.Scripts.Stage.Interfaces;
using Assets.Scripts.UI.MiniMap;
using System;
using System.Linq;
using UnityEngine;
using static Assets.Editor.Editor;

namespace Assets.Scripts.Stage
{
    internal class StageController : MonoBehaviour, IStageController
    {
        private Player _player;
        private Camera _camera;
        public StageTileElement _currentElement { get; set; }
        private StageTileElement _endElement;
        public Action OnLastStageLeft;
        private StageTileElementHolder _currentStageLevel;
        private bool _runtimeActivated;
        private MiniMapElement _playerMinimapElement;
        private void Awake()
        {

            Singleton = this;
        }
        public static StageController Singleton { get; private set; }
        private void OnEnable()
        {
            LevelCompositeRoot.Instance.LevelInfo.OnRegisterSubscribeAndCallOnExist(OnEntitySpawn);
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDamaged += OnEntityDamaged;
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDie += OnEntityDie;
            _camera = Camera.main;
            Editor.Editor.Instance._spaceController.OnSpaceChanged = OnSpaceChanged;
        }
        private void OnDisable()
        {
            LevelCompositeRoot.Instance.LevelInfo.OnRegisterUnsubscribe(OnEntitySpawn);
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDamaged -= OnEntityDamaged;
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDie -= OnEntityDie;
        }
        private void OnEntitySpawn(Entity entity, bool isSpawned)
        {
            if (isSpawned && entity is IStatic == false) entity.HousingElement = _currentElement;
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
            var entities = NavigationUtil.GetEntitiesOfTypeInsideOriginTile(_player.TargetType, _player);
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
                LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.NONE);
            }
        }
        private void OnLastStageCompleted()
        {
            OnLastStageLeft.Invoke();
        }
        public void RestoreRuntime()
        {
            LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.RUNTIME);
            Instance._inputManager.SetActivePreviewEntity(false);
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
            LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.EDIT);
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
            UpdateMinimap(direction);
            LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.EDIT);
            Instance.Dockspace.DisableDockspace();
            for (int i = 0; i < temp.Elements.Length; i++) if (temp.Elements[i].Direction == direction) SetCurrentElement(temp.Elements[i].Element);
        }
        private void UpdateMinimap(Direction direction)
        {
            var map = LevelCompositeRoot.Instance.MiniMap;
            var newElement = _playerMinimapElement.GetCellInDirection(direction);
            newElement.SetState(MiniMapElement.State.PLAYER);
            
            if(_playerMinimapElement != null)
                _playerMinimapElement.SetState(MiniMapElement.State.NONE);
            map.SetCurrentElement(newElement);
            newElement.RevealRelativeElements();
            _playerMinimapElement = map.GetCurrentMapElement();
        }
        private void InitPlayer()
        {
            _player = Instance.PlayerSpace.InitPlayer(_currentStageLevel.InitialElement.PlayerPosition.transform.position);
        }
        public void StartStageLevel(int floor, StageTileElementHolder currentStageLevel, bool init = false)
        {
            var map = LevelCompositeRoot.Instance.MiniMap;
            map.ResetMap();
            Editor.Editor.Instance._runtimeSystem.SetRuntimeDatabase(currentStageLevel.SpecificRuntimeDatabase);
            Editor.Editor.Instance._editSystem.SetEditorDatabase(currentStageLevel.SpecificEditorDatabase);

            _playerMinimapElement = map.GetCurrentMapElement();
            _playerMinimapElement.SetState(MiniMapElement.State.PLAYER);
            currentStageLevel.FillMap();
            _playerMinimapElement.RevealRelativeElements();


            UIEventCompositeRoot.Instance.LevelTitle.EnableText($"Floor {floor}");
            _currentStageLevel = currentStageLevel;
            if (init) InitPlayer();
            _endElement = currentStageLevel.EndElement;
            SetCurrentElement(currentStageLevel.InitialElement);
            Instance._levelActions.OnButtonSwitched = OnButtonSwitched;
            Instance._levelActions.ActivateButton(ButtonType.MOVE_NEXT_LEVEL);
        }
        private void SetCurrentElement(StageTileElement element)
        {
            _currentElement = element;
            LevelCompositeRoot.Instance.LevelSoundController.SetRuntimeTheme(_currentElement.SpecificRuntimeSoundtrack);

            _camera.transform.position = new Vector3(_currentElement.transform.position.x, _currentElement.transform.position.y, _camera.transform.position.z);
            _player.transform.position = _currentElement.PlayerPosition.position;
            _player.HousingElement = _currentElement;
            Instance._spaceController.SetMaxSpaceReqiured(element.EditorSpaceRequired);
            _camera.orthographicSize = _currentElement.CameraSize;
            if (LevelCompositeRoot.Instance.Runner.CurrentMode == Editor.GameMode.RUNTIME) LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.RUNTIME);
        }
        private void EnableDockspace()
        {
            var availableDirections = _currentElement.Elements.Select(x => x.Direction).ToArray();
            Instance.Dockspace.EnableDockspace(availableDirections, this);
        }
    }
}
