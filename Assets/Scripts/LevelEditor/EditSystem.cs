using Assets.Editor;
using Assets.Scripts.Databases.dto.Runtime;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Placeable;
using System.Collections.Generic;
using UnityEngine;

internal class EditSystem : PlacementSystem
{
    private List<GameObject> placedEntities = new();

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && editor.GameModeIs(Editor.GameMode.EDIT))
        {
            OnPlace?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && editor.GameModeIs(Editor.GameMode.EDIT))
        {
            OnDelete?.Invoke();
        }
    }
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < database.Entities.Count; i++)
        {
            EntityHolder obj = Instantiate(entityHolder, editor.EntityHolderContainer);
            obj.Init(i, database, this);
        }
    }
    private void PlaceEntity()
    {
        if (editor._inputManager.IsPointerOverUI()) return;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(editor._inputManager.GetCursorPosition());

        bool placementValidity = CheckPlacementValidity(gridPos, selectedEntityIndex);
        if (placementValidity == false) return;

        var model = database.Entities[selectedEntityIndex].GetModel();
        var entity = Instantiate(model.Entity, grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y)) + new Vector3(model.Size, model.Size) / 2, Quaternion.identity);

        placedEntities.Add(entity.gameObject);
        ((IPlaceable)entity).OnContruct();

        gridData.AddEntityAt(gridPos, model.Size,
            selectedEntityIndex, placedEntities.Count - 1);
    }
    private void DeleteEntity()
    {
        GridData selectedData = null;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(editor._inputManager.GetCursorPosition());

        if (gridData.CanPlaceObjectAt(gridPos, 1) == false)
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
                    placedEntities[id].GetComponent<IPlaceable>().OnDeconstruct();
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
        var model = database.Entities[selectedEntityIndex].GetModel();
        if (editor._spaceController.IsOverloaded(model.EditorSpaceRequired.SpaceRequired)) return false;
        Vector3Int[] posns = new Vector3Int[model.Size * model.Size];

        for (int x = 1; x <= model.Size; x++)
        {
            for (int y = 1; y <= model.Size; y++)
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
        return gridData.CanPlaceObjectAt(gridPosition, model.Size);
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
