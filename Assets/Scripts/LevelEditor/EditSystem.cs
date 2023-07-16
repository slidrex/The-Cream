using System.Collections.Generic;
using UnityEngine;

public class EditSystem : PlacementSystem
{
    private List<GameObject> placedEntities = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && editor.GameModeIs(GameMode.EDIT))
        {
            OnPlace?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && editor.GameModeIs(GameMode.EDIT))
        {
            OnDelete?.Invoke();
        }
    }
    protected override void Start()
    {
        base.Start();
        editor.SetGamemode(GameMode.EDIT);
        for (int i = 0; i < database.Entities.Count; i++)
        {
            EntityHolder obj = Instantiate(entityHolder, editor.Parent);
            obj.Init(i, database, this);
        }
    }
    private void PlaceEntity()
    {
        if (editor._inputManager.IsPointerOverUI()) return;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(editor._inputManager.GetCursorPosition());

        bool placementValidity = CheckPlacementValidity(gridPos, selectedEntityIndex);
        if (placementValidity == false) return;

        GameObject entity = Instantiate(database.Entities[selectedEntityIndex].Prefab, grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y)), Quaternion.identity);
        placedEntities.Add(entity);

        gridData.AddEntityAt(gridPos, database.Entities[selectedEntityIndex].Size,
            selectedEntityIndex, placedEntities.Count - 1);
    }
    private void DeleteEntity()
    {
        GridData selectedData = null;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(editor._inputManager.GetCursorPosition());

        if (gridData.CanPlaceObjectAt(gridPos, Vector2Int.one) == false)
        {
            selectedData = gridData;
        }
        if (selectedData != null)
        {
            int id = selectedData.GetEntityIDAt(gridPos);
            if (id < 0) return;
            else
            {
                if (placedEntities.Count <= id || placedEntities[id] == null) return;
                else
                {
                    selectedData.RemoveObjectAt(gridPos);
                    Destroy(placedEntities[id]);
                    placedEntities[id] = null;
                }
            }
        }
    }
    public bool CheckPlacementValidity(Vector2Int gridPosition, int selectedEntityIndex)
    {
        if (selectedEntityIndex < 0) return false;
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
            Vector3Int pos = new Vector3Int(gridPosition.x, gridPosition.y, 0) + p - new Vector3Int(1, 1, 0);
            if (editor.LimitingTileMap.HasTile(pos))
            {
                return false;
            }
        }
        return gridData.CanPlaceObjectAt(gridPosition, database.Entities[selectedEntityIndex].Size);
    }
    public void SignMethods(bool active)
    {
        if (active == true)
        {
            OnPlace += PlaceEntity;
            OnDelete += DeleteEntity;
        }
        else
        {
            OnPlace -= PlaceEntity;
            OnDelete -= DeleteEntity;
        }
    }
    public void OnDisable()
    {
        OnPlace -= PlaceEntity;
        OnDelete -= DeleteEntity;
    }
}
