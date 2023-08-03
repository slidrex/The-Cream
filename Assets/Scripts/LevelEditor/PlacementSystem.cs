using Assets.Editor;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

internal abstract class PlacementSystem : MonoBehaviour
{
    [SerializeField] protected EntityDatabase database;
    protected GridData gridData = new();
    protected Grid grid;
    protected Tilemap limitingTilemap;
    protected int selectedEntityIndex = -1;
    public Action OnPlace, OnDelete;
    protected Editor editor;
    protected virtual void Awake()
    {
        editor = Editor.Instance;
        grid = editor.GetGrid();
        limitingTilemap = editor.LimitingTileMap;
    }
    public void SetCurrentEntityID(int id)
    {
        selectedEntityIndex = id;
        if (selectedEntityIndex < 0)
        {
            Debug.LogError($"нет такого id: {id}");
            return;
        }
        OnAfterSetCurrentEntityId();
    }
    protected virtual void OnAfterSetCurrentEntityId()
    {

    }
    public abstract void ClearContainer();
    public abstract void FillContainer();
    public void SetNewEntityDatabase(EntityDatabase database) =>
        this.database = database;
    public int GetSelectedEntityIndex() => selectedEntityIndex;
}
