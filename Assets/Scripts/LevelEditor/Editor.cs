using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Level.Stages;
using Assets.Scripts.LevelEditor.RuntimeSpace.Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Editor
{
    internal class Editor : MonoBehaviour
    {
        [SerializeField] private GameObject runtime, editor;
        public static Editor Instance { get; private set; }
        [field: SerializeField] public Transform EditorHolderContainer { get; private set; }
        [field: SerializeField] public Transform RuntimeHolderContainer { get; private set; }
        [field: SerializeField] public Transform RuntimePlayerContainer { get; private set; }
        [field: SerializeField] public Tilemap LimitingTileMap { get; private set; }
        [field: SerializeField] public Tilemap PlacementTileMap { get; private set; }
        [field: SerializeField] public EditSystem _editSystem { get; private set; }
        [field: SerializeField] public RuntimeSystem _runtimeSystem { get; private set; }
        [field: SerializeField] public InputManager _inputManager { get; private set; }
        [field: SerializeField] public LevelActions _levelActions { get; private set; }
        [field: SerializeField] public SpaceController _spaceController { get; private set; }
        [field: SerializeField] internal Dockspace Dockspace { get; private set; }
        [field: SerializeField] internal PlayerRuntimeSpace PlayerSpace { get; private set; }
        public GameMode CurrentGamemode { get; private set; } = GameMode.NONE;
        private Grid grid;

        private void Awake()
        {
            Instance = this;
            grid = FindObjectOfType<Grid>();
            PlayerSpace.OnConfigure();
        }
        public bool GameModeIs(GameMode mode) => CurrentGamemode == mode;
        public void SetGamemode(GameMode gamemode)
        {
            CurrentGamemode = gamemode;
            ClearContent();
            SwitchScreen();
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
                        LevelCompositeRoot.Instance.Runner.StopLevel();
                        _runtimeSystem.SignMethods(false);
                        break;
                    }
                case GameMode.RUNTIME:
                    {
                        _editSystem.SignMethods(false);
                        LevelCompositeRoot.Instance.Runner.RunLevel();
                        _runtimeSystem.SignMethods(true);
                        break;
                    }
            }
        }

        private void SwitchScreen()
        {
            runtime.SetActive(false);
            editor.SetActive(false);
            if(GameModeIs(GameMode.EDIT)) editor.SetActive(true);
            else if(GameModeIs(GameMode.RUNTIME)) runtime.SetActive(true);

        }
        public void ClearContent()
        {
            EntityHolder[] objs = EditorHolderContainer.GetComponentsInChildren<EntityHolder>();
            foreach (EntityHolder obj in objs)
            {
                Destroy(obj.gameObject);
            }
        }
        public Grid GetGrid() => grid;
        public enum GameMode
        {
            NONE, EDIT, RUNTIME
        }
    }
}