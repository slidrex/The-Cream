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
    public Action<Vector2> OnPlace;
    public Action OnDelete;
    protected Editor editor;
    protected virtual void Awake()
    {
        editor = Editor.Instance;
        grid = editor.GetGrid();
        limitingTilemap = editor.LimitingTileMap;
    }
    public void SetCurrentEntityID(ObjectHolder holder, int id, bool clickedByIcon = false)
    {
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
