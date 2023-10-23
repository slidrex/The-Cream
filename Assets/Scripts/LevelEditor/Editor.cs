using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Databases.Database_providers;
using Assets.Scripts.Databases.Model.Character;
using Assets.Scripts.Databases.Model.Player;
using Assets.Scripts.GameProgress;
using Assets.Scripts.Level;
using Assets.Scripts.Level.Stages;
using Assets.Scripts.Level.Tilemap;
using Assets.Scripts.LevelEditor;
using Assets.Scripts.LevelEditor.RuntimeSpace.Player;
using Assets.Scripts.LevelEditor.RuntimeSpace.PlayerUtil;
using Assets.Scripts.LevelEntry;
using Assets.Scripts.PlatformConfig;
using Assets.Scripts.Sound.Soundtrack;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Tilemaps;

namespace Assets.Editor
{
    internal class Editor : MonoBehaviour
    {
        [SerializeField] private AudioClip _editorTheme;
        [SerializeField] private GameObject runtime, editor;
        public static Editor Instance { get; private set; }
        [field: SerializeField] public PlayerMarks PlayerMarks { get; private set; }
        [field: SerializeField] public Transform EditorHolderContainer { get; private set; }
        [field: SerializeField] public Transform RuntimeHolderContainer { get; private set; }
        [field: SerializeField] public Transform RuntimePlayerContainer { get; private set; }
        public Tilemap LimitingTileMap { get; private set; }
        public Tilemap PlacementTileMap { get; private set; }
        public PreviewManager PreviewManager;
        public Action OnAfterPlayerInitialized;
        [field: SerializeField] public EditSystem _editSystem { get; private set; }
        [field: SerializeField] public RuntimeSystem _runtimeSystem { get; private set; }
        [field: SerializeField] public InputManager _inputManager { get; private set; }
        [field: SerializeField] public LevelActions _levelActions { get; private set; }
        [field: SerializeField] public SpaceController _spaceController { get; private set; }
        [field: SerializeField] internal Dockspace Dockspace { get; private set; }
        [field: SerializeField] public PlayerRuntimeSpace PlayerSpace { get; private set; }
        public GameMode CurrentGamemode { get; private set; } = GameMode.NONE;
        private Grid grid;
        public static CharacterDatabaseModel.CharacterID SelectedCharacterId;
        [SerializeField] private CharacterModel _specificCharacterModel;
        public static Camera Camera { get; set; }

		private void Awake()
        {
            Instance = this;
            Camera = FindObjectOfType<Camera>();
            
            FindObjectOfType<LevelEntryPoint>().OnHolderActivate += (StageTileElementHolder holder) => { PlacementTileMap = holder.PlacementTileMap; LimitingTileMap = holder.LimitingTileMap; };
            grid = FindObjectOfType<Grid>();
            if (PersistentData.IsNewbie)
            {
                Analytics.CustomEvent("time_in_menu_before_start_first_game_in_seconds", new Dictionary<string, object>() { ["time"] = Time.realtimeSinceStartup });
            }
			PersistentData.IsNewbie = false;
			Analytics.CustomEvent("character_pick", new Dictionary<string, object>() { ["character_name"] = SelectedCharacterId.ToString() });

			PlayerSpace.InitPlayer(_specificCharacterModel == null ? GameLevelDatabaseProvider.Instance.CharacterDatabase.GetCharacter(SelectedCharacterId) : _specificCharacterModel);
			
            PlayerSpace.OnConfigure();
        }
        private void OnEnable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += SetGamemode;
        }
        private void OnDisable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= SetGamemode;
        }
        public bool GameModeIs(GameMode mode) => CurrentGamemode == mode;
        private void SetGamemode(GameMode gamemode)
        {
            CurrentGamemode = gamemode;
            SwitchScreen();
            ClearContainer();
            FillContainer();
            switch (gamemode)
            {
                case GameMode.NONE:
                    {
                        _editSystem.SignMethods(false);
                        _runtimeSystem.SignMethods(false);
                        break;
                    }
                case GameMode.EDIT:
                    {
                        _editSystem.SignMethods(true);
                        _runtimeSystem.SignMethods(false);
                        break;
                    }
                case GameMode.RUNTIME:
                    {
                        _editSystem.SignMethods(false);
                        _runtimeSystem.SignMethods(true);
                        break;
                    }
            }
        }
        public void FillContainer()
        {
            if (GameModeIs(GameMode.EDIT))
                _editSystem.FillContainer();
            else if(GameModeIs(GameMode.RUNTIME))
                _runtimeSystem.FillContainer();
        }
        public void ClearContainer()
        {
            if(GameModeIs(GameMode.RUNTIME))
                _editSystem.ClearContainer();
            else if(GameModeIs(GameMode.EDIT))
                _runtimeSystem.ClearContainer();
            else
            {
                _editSystem.ClearContainer();
                _runtimeSystem.ClearContainer();
            }
        }
        private void SwitchScreen()
        {
            runtime.SetActive(false);
            editor.SetActive(false);
            if(GameModeIs(GameMode.EDIT)) editor.SetActive(true);
            else if(GameModeIs(GameMode.RUNTIME)) runtime.SetActive(true);

        }
        public Grid GetGrid() => grid;
    }
    public enum GameMode
    {
        UNASSIGNED, NONE, EDIT, RUNTIME
    }
}