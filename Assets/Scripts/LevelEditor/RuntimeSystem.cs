using Assets.Editor;
using UnityEngine;

internal class RuntimeSystem : PlacementSystem
{
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < database.Entities.Count; i++)
        {
            EntityHolder obj = Instantiate(entityHolder, editor.EntityHolderContainer);
            obj.Init(i, database, this);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && editor.GameModeIs(Editor.GameMode.RUNTIME))
        {
            OnPlace?.Invoke();
        }
    }
    private void PlaceEntity()
    {
        if (editor._inputManager.IsPointerOverUI()) return;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(editor._inputManager.GetCursorPosition());
        bool validity = CheckPlacementValidity(gridPos, selectedEntityIndex);
        var model = database.Entities[selectedEntityIndex].GetModel();

        if (validity == true)
        {
            var entity = Instantiate(model.Entity, grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y)) + new Vector3(model.Size, model.Size) / 2, Quaternion.identity);
        }
    }
    public bool CheckPlacementValidity(Vector2Int gridPosition, int selectedEntityIndex)
    {
        if(selectedEntityIndex < 0) return false;
        if (editor.LimitingTileMap.HasTile(new Vector3Int(gridPosition.x, gridPosition.y)))
        {
            return false;
        }
        return true;
    }
    public void SignMethods(bool active)
    {
        if (active == true)
        {
            OnPlace += PlaceEntity;
        }
        else
        {
            OnPlace -= PlaceEntity;
        }
    }
    private void OnDisable()
    {
        OnPlace -= PlaceEntity;
    }
}
