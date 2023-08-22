using Assets.Editor;
using Assets.Scripts.Databases.dto.Runtime;
using Assets.Scripts.Databases.LevelDatabases;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.LevelEditor;
using System.Collections.Generic;
using UnityEngine;

internal class EditSystem : PlacementSystem
{
    [SerializeField] private EditorEntityDatabase _editorDatabase;
    private List<BaseEntity> placedEntities = new();
    [SerializeField] private List<EditorEntityHolder> holders = new();
    private EditorEntityHolder entityHolder;
    private void Update()
    {
        if(OnPlace != null)
        {
            if (Input.GetKey(KeyCode.Mouse0) && selectedEntityIndex != -1 && Editor.Instance.GameModeIs(GameMode.EDIT))
            {
                Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(OnPlace, selectedHolder) { NotDeselectOnChoose = true, Status = PreviewManager.PreviewStatus.DISABLED });
            }
            else if (Input.GetKey(KeyCode.Mouse1) && Editor.Instance.GameModeIs(GameMode.EDIT))
            {
                OnDelete?.Invoke();
            }
        }
    }
    protected override void OnAfterSetCurrentEntityId()
    {
        
        Editor.Instance._inputManager.SetPreviewSprite(_editorDatabase.Entities[selectedEntityIndex].GetModel().Icon);
    }
    protected override void Awake()
    {
        foreach(var e in _editorDatabase.Entities)
        {
            e.Configure();
        }
        entityHolder = Resources.Load<EditorEntityHolder>("UI/EditorEntityHolder");
        base.Awake();
    }
    public override void FillContainer()
    {
        for (int i = 0; i < _editorDatabase.Entities.Count; i++)
        {
            EditorEntityHolder obj = Instantiate(entityHolder, editor.EditorHolderContainer);
            obj.Init(i, _editorDatabase, this, KeyCode.None);
            holders.Add(obj);
        }
    }
    public override void ClearContainer()
    {
        for (int i = 0; i < holders.Count; i++)
        {
            Destroy(holders[i].gameObject);
        }
        holders.Clear();
    }
    private void PlaceEntity(Vector2 cursorPos)
    {
        if (editor._inputManager.IsPointerOverUI()) return;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(editor._inputManager.GetCursorPosition());

        bool placementValidity = CheckPlacementValidity(gridPos, selectedEntityIndex);
        if (placementValidity == false) return;

        var model = (EditorEntityModel.EditorModel)_editorDatabase.Entities[selectedEntityIndex].GetModel();
        var entity = Instantiate(model.Entity, grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y)) + new Vector3(model.Size, model.Size) / 2, Quaternion.identity);

        placedEntities.Add(entity);

        ((IPlaceable)entity).OnContruct();
        editor._spaceController.ChangeSpace((entity as IEditorSpaceRequired).SpaceRequired);

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
                var entity = placedEntities[id];
                if (placedEntities.Count <= id || entity == null) return;
                else
                {
                    entity.GetComponent<IPlaceable>().OnDeconstruct();
                    editor._spaceController.ChangeSpace(-(entity as IEditorSpaceRequired).SpaceRequired);
                    selectedData.RemoveObjectAt(gridPos);
                    Destroy(entity.gameObject);
                    placedEntities[id] = null;
                }
            }
        }
    }
    public bool CheckPlacementValidity(Vector2Int gridPosition, int selectedEntityIndex)
    {
        if (selectedEntityIndex < 0) return false;
        int count = 0;
        var model = (EditorEntityModel.EditorModel)_editorDatabase.Entities[selectedEntityIndex].GetModel();
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
            if (editor.LimitingTileMap.HasTile(pos) || !editor.PlacementTileMap.HasTile(pos))
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
