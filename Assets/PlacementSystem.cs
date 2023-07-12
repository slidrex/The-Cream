using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private EntityDatabase database;
    [SerializeField] private Tilemap limitingTileMap;
    [SerializeField] private Transform parent;
    private EntityHolder entityHolder;
    private int selectedEntityIndex = -1;
    public Action OnPlace;
    private GridData gridData = new();
    private List<GameObject> placedEntities = new();
    private const byte placementLayer = 6;

    private void Start()
    {
        Editor.Instance.SetGamemode(GameMode.EDIT);
        entityHolder = Resources.Load<EntityHolder>("UI/EntityHolder");
        for (int i = 0; i < database.Entities.Count; i++)
        {
            EntityHolder obj = Instantiate(entityHolder, parent);
            obj.Init(database.Entities[i].ID);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Editor.Instance.GameModeIs(GameMode.EDIT))
        {
            OnPlace?.Invoke();
        }
    }
    public void SetCurrentEntityID(int id)
    {
        selectedEntityIndex = database.Entities.FindIndex(data => data.ID == id);
        if (selectedEntityIndex < 0)
        {
            Debug.LogError($"нет такого id: {id}");
            return;
        }
    }
    public void PlaceEntity()
    {
        if (Editor.Instance._inputManager.IsPointerOverUI()) return;

        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(Editor.Instance._inputManager.GetCursorPosition());

        bool placementValidity = CheckPlacementValidity(gridPos, selectedEntityIndex);
        if (placementValidity == false) return;

        GameObject entity = Instantiate(database.Entities[selectedEntityIndex].Prefab, grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y)), Quaternion.identity);
        placedEntities.Add(entity);
        gridData.AddEntityAt(gridPos, database.Entities[selectedEntityIndex].Size,
            database.Entities[selectedEntityIndex].ID, placedEntities.Count - 1);
    }
    private bool CheckPlacementValidity(Vector2Int gridPosition, int selectedEntityIndex)
    {
        int count = 0;
        Vector3Int[] posns = new Vector3Int[database.Entities[selectedEntityIndex].Size.x * database.Entities[selectedEntityIndex].Size.y];
        for (int x = 1; x <= database.Entities[selectedEntityIndex].Size.x; x++)
        {
            for (int y = 1; y <= database.Entities[selectedEntityIndex].Size.y; y++)
            {
                posns[count] = new Vector3Int(x, y, 0);
                count++;
            }
        }
        foreach (Vector3Int p in posns)
        {
            Debug.DrawLine(new Vector3(gridPosition.x, gridPosition.y), p, Color.red);
            Vector3Int pos = new Vector3Int(gridPosition.x, gridPosition.y, 0) + p - new Vector3Int(1, 1, 0);
            if (limitingTileMap.HasTile(pos))
            {
                return false;
            }
        }
        return gridData.CanPlaceObjectAt(gridPosition, database.Entities[selectedEntityIndex].Size);
    }
    public int GetSelectedEntityIndex() => selectedEntityIndex;
    public Grid GetGrid() => grid;
}
