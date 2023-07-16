using UnityEngine;

public class RuntimeSystem : PlacementSystem
{
    protected override void Start()
    {
        base.Start();
        editor.SetGamemode(GameMode.RUNTIME);
        for (int i = 0; i < database.Entities.Count; i++)
        {
            EntityHolder obj = Instantiate(entityHolder, editor.Parent);
            obj.Init(i, database, this);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && editor.GameModeIs(GameMode.RUNTIME))
        {
            OnPlace?.Invoke();
        }
    }
    private void PlaceEntity()
    {
        if (editor._inputManager.IsPointerOverUI()) return;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(editor._inputManager.GetCursorPosition());
        bool validity = CheckPlacementValidity(gridPos, selectedEntityIndex);
        if (validity == true)
        {
            GameObject entity = Instantiate(database.Entities[selectedEntityIndex].Prefab, grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y)), Quaternion.identity);
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
