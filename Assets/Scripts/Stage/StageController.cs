using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Move;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Moving;
using Assets.Scripts.Entities.Stats.Interfaces.States;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Structures.Portal;
using Assets.Scripts.Functions;
using Assets.Scripts.Sound;
using Assets.Scripts.Stage.Interfaces;
using Assets.Scripts.UI.Menu.Advanced;
using Assets.Scripts.UI.MiniMap;
using System;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using static Assets.Editor.Editor;

namespace Assets.Scripts.Stage
{
    internal class StageController : MonoBehaviour, IStageController
    {
        private Player _player;
        private CinemachineVirtualCamera _camera;
		[SerializeField] private AudioClip _transitionSound;
		public StageTileElement _currentElement { get; set; }
        private StageTileElement _endElement;
        public Action OnLastStageLeft;
        private StageTileElementHolder _currentStageLevel;
        private bool _runtimeActivated;
        public Action OnDockspaceMoved;
        private MiniMapElement _playerMinimapElement;
        public Action OnStageCleanedUp;
        public Action OnAfterStageStarted;
        public bool DisableAutoactivateWave { get; set; }
        private void Awake()
        {
			OnDockspaceMoved += () => SoundCompositeRoot.Instance.SoundPlayer.Play(_transitionSound);
			Instance._levelActions.OnButtonSwitched = OnButtonSwitched;
			LevelCompositeRoot.Instance.Runner.TriggerResettableIterfaces();
			Singleton = this;
        }

        private void OnGameModeChanged(GameMode mode)
        {
            if (mode == GameMode.EDIT)
            {
                _camera.Follow = null;
                Vector3 pos = _currentElement.transform.position;
                _camera.ForceCameraPosition(new Vector3(pos.x, pos.y, _camera.transform.position.z), Quaternion.identity);
            }
            else
            {
                _camera.Follow = _player.transform;
                _camera.m_Lens.OrthographicSize = _currentElement.RuntimeCameraSize;
            }
        }
        public static StageController Singleton { get; private set; }
        private void OnEnable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnGameModeChanged;

            LevelCompositeRoot.Instance.LevelInfo.OnRegisterSubscribeAndCallOnExist(OnEntitySpawn);
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDamaged += OnEntityDamaged;
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDie += OnEntityDie;
            _camera = FindObjectOfType<CinemachineVirtualCamera>();
            Editor.Editor.Instance._spaceController.OnSpaceChanged += OnSpaceChanged;
        }
        private void OnDisable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnGameModeChanged;
			Editor.Editor.Instance._spaceController.OnSpaceChanged -= OnSpaceChanged;
			LevelCompositeRoot.Instance.LevelInfo.OnRegisterUnsubscribe(OnEntitySpawn);
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDamaged -= OnEntityDamaged;
            LevelCompositeRoot.Instance.LevelInfo.OnEntityDie -= OnEntityDie;
        }
        private void OnEntitySpawn(Entity entity, bool isSpawned)
        {
            if (isSpawned && entity.IsStatic == false) entity.HousingElement = _currentElement;
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
				OnStageCleanedUp?.Invoke();
                _currentElement.IsEmpty = true;
				if (!DisableAutoactivateWave)
                    ActivateWave();
			}
        }
        public void ActivateWave()
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
                    if (ents[i].IsStatic == false)
                        Destroy(ents[i].gameObject);
                }
            }

			LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.NONE);

        }
        private void OnLastStageCompleted()
        {
            OnLastStageLeft?.Invoke();
        }
        public void RestoreRuntime()
        {
            LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.RUNTIME);
            TriggerEntityResets(false);

			Instance._inputManager.SetActivePreviewEntity(false);
            Instance._levelActions.ActivateButton(ButtonType.STOP_RUNTIME);
            _runtimeActivated = true;
        }
        private void TriggerEntityResets(bool onCancelled)
        {
            foreach(var entity in LevelCompositeRoot.Instance.LevelInfo.RuntimeEntities)
            {
                if (onCancelled) entity.OnWaveCancelled();
                else entity.OnWaveStarted();
            }
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
			TriggerEntityResets(true);
			LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.EDIT);
            Instance._levelActions.ActivateButton(ButtonType.NONE);
            OnSpaceChanged(Instance._spaceController.CurrentSpaceReqiured);
            _camera.m_Lens.OrthographicSize = _currentElement.EditCameraSize;
        }
        private void OnButtonSwitched(ButtonType type)
        {
            if (type == ButtonType.MOVE_NEXT_LEVEL) EnableDockspace();
            else Instance.Dockspace.DisableDockspace();
        }
        public void Move(Direction direction)
        {
            var temp = _currentElement;
            LevelCompositeRoot.Instance.Runner.TriggerResettableIterfaces();
            UpdateMinimap(direction);
            for (int i = 0; i < temp.Elements.Count; i++) if (temp.Elements[i].Direction == direction)
                {
                    SetCurrentElement(temp.Elements[i].Element);
                }

            OnDockspaceMoved?.Invoke();

            if (_currentElement.IsEmpty == false)
            {
                LevelCompositeRoot.Instance.Runner.SetGameMode(GameMode.EDIT);
                Instance.Dockspace.DisableDockspace();
            }
            else EnableDockspace();
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
            var camera = FindObjectOfType<CinemachineVirtualCamera>();
            camera.m_Follow = _player.transform;
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

            SetFloorTextIndex(floor);

			_currentStageLevel = currentStageLevel;
            if (init) InitPlayer();
            _endElement = currentStageLevel.EndElement;
            SetCurrentElement(currentStageLevel.InitialElement);
            Instance._levelActions.ActivateButton(ButtonType.MOVE_NEXT_LEVEL);
            OnAfterStageStarted?.Invoke();
		}
        private void SetFloorTextIndex(int floor)
        {
            UIEventCompositeRoot.Instance.LevelTitle.gameObject.GetComponent<ListIndex>().Index = floor;
			UIEventCompositeRoot.Instance.LevelTitle.EnableText();

        }
        private void SetCurrentElement(StageTileElement element)
        {
            _currentElement = element;
            LevelCompositeRoot.Instance.LevelSoundController.SetRuntimeTheme(_currentElement.SpecificRuntimeSoundtrack);

            _camera.transform.position = new Vector3(_currentElement.transform.position.x, _currentElement.transform.position.y, _camera.transform.position.z);
            _player.transform.position = _currentElement.PlayerPosition.position;
            
            _player.HousingElement = _currentElement;
            Instance._spaceController.SetMaxSpaceReqiured(element.EditorSpaceRequired);
            _camera.m_Lens.OrthographicSize = _currentElement.EditCameraSize;
        }
        private void EnableDockspace()
        {
            var availableDirections = _currentElement.Elements.Select(x => x.Direction).ToArray();
            Instance.Dockspace.EnableDockspace(availableDirections, this);
        }
    }
}
