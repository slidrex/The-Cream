using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class PlacementSystem : MonoBehaviour
{
    [SerializeField] protected EntityDatabase database;
    protected GridData gridData = new();
    protected Grid grid;
    protected Tilemap limitingTilemap;
    protected EntityHolder entityHolder;
    protected int selectedEntityIndex = -1;
    public Action OnPlace, OnDelete;
    protected Editor editor;
    protected virtual void Start()
    {
        editor = Editor.Instance;
        grid = editor.GetGrid();
        limitingTilemap = editor.LimitingTileMap;
        entityHolder = Resources.Load<EntityHolder>("UI/EntityHolder");
    }
    public void SetCurrentEntityID(int id)
    {
        selectedEntityIndex = id;
        if (selectedEntityIndex < 0)
        {
            Debug.LogError($"нет такого id: {id}");
            return;
        }
    }
    public int GetSelectedEntityIndex() => selectedEntityIndex;
}
