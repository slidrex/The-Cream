using UnityEngine;

public class Editor : MonoBehaviour
{
    public static Editor Instance { get; private set; }
    [field: SerializeField] public PlacementSystem _placementSystem { get; private set; }
    [field: SerializeField] public InputManager _inputManager { get; private set; }
    public GameMode CurrentGamemode { get; private set; } = GameMode.NONE;

    private void Awake()
    {
        Instance = this;
    }
    public bool GameModeIs(GameMode mode) => CurrentGamemode == mode;
    public void SetGamemode(GameMode gamemode)
    {
        CurrentGamemode = gamemode;
        switch (gamemode)
        {
            case GameMode.NONE:
                {
                    _placementSystem.OnPlace -= _placementSystem.PlaceEntity;
                    break;
                }
            case GameMode.EDIT:
                {
                    _placementSystem.OnPlace += _placementSystem.PlaceEntity;
                    break;
                }
            case GameMode.RUNTIME:
                {
                    _placementSystem.OnPlace -= _placementSystem.PlaceEntity;
                    break;
                }
        }
    }
}
public enum GameMode
{
    NONE, EDIT, RUNTIME
}