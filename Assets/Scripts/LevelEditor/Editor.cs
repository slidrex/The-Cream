using UnityEngine;
using UnityEngine.Tilemaps;

public class Editor : MonoBehaviour
{
    public static Editor Instance { get; private set; }
    [field: SerializeField] public Transform Parent { get; private set; }
    [field: SerializeField] public Tilemap LimitingTileMap { get; private set; }
    [field: SerializeField] public EditSystem _editSystem { get; private set; }
    [field: SerializeField] public RuntimeSystem _runtimeSystem { get; private set; }
    [field: SerializeField] public InputManager _inputManager { get; private set; }
    [field: SerializeField] public LevelActions _levelActions { get; private set; }
    public GameMode CurrentGamemode { get; private set; } = GameMode.NONE;
    private Grid grid;

    private void Awake()
    {
        Instance = this;
        grid = FindObjectOfType<Grid>();
    }
    public bool GameModeIs(GameMode mode) => CurrentGamemode == mode;
    public void SetGamemode(GameMode gamemode)
    {
        CurrentGamemode = gamemode;
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
    public void ClearContent()
    {
        EntityHolder[] objs = Parent.GetComponentsInChildren<EntityHolder>();
        foreach (EntityHolder obj in objs)
        {
            Destroy(obj.gameObject);
        }
    }
    public Grid GetGrid() => grid;
}
public enum GameMode
{
    NONE, EDIT, RUNTIME
}