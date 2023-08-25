using Assets.Editor;
using Assets.Scripts.Databases.LevelDatabases;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.LevelEditor;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

internal abstract class PlacementSystem : MonoBehaviour
{
    protected GridData gridData = new();
    protected Grid grid;
    protected Tilemap limitingTilemap;
    protected int selectedEntityIndex = -1;
    protected ObjectHolder selectedHolder;
    protected Action<Vector2> OnPlace;
    protected delegate bool OnDelete();
    protected OnDelete onDelete;
    protected Editor editor;
    protected virtual void Awake()
    {
        editor = Editor.Instance;
        grid = editor.GetGrid();
        limitingTilemap = editor.LimitingTileMap;
    }
    protected virtual bool AllowUsingEntityID(int id)
    {
        return true;
    }
    public void SetCurrentEntityID(ObjectHolder holder, int id, bool clickedByIcon = false)
    {
        if (!AllowUsingEntityID(id))
        {
            selectedEntityIndex = -1;
            return;
        }
        selectedHolder = holder;
        selectedEntityIndex = id;
        if (selectedEntityIndex < 0)
        {
            Debug.LogError($"нет такого id: {id}");
            return;
        }
        if(OnPerformActionValidation())
            Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(OnPlace, holder) { Status = clickedByIcon ? PreviewManager.PreviewStatus.ENABLED : PreviewManager.PreviewStatus.AUTO});
        OnAfterSetCurrentEntityId();
    }
    protected virtual bool OnPerformActionValidation()
    {
        return true;
    }
    protected virtual void OnAfterSetCurrentEntityId()
    {

    }
    public abstract void ClearContainer();
    public abstract void FillContainer();
    public int GetSelectedEntityIndex() => selectedEntityIndex;
}
