using UnityEngine;
using System.Collections.Generic;
using System;

public class GridData
{
    private Dictionary<Vector2Int, PlacementData> placedEntities = new();

    public void AddEntityAt(Vector2Int gridPosition, Vector2Int entitySize, int id, int entityIndex)
    {
        List<Vector2Int> positionToOccupy = CalculatePositions(gridPosition, entitySize);
        PlacementData data = new(positionToOccupy, id, entityIndex);
        foreach (Vector2Int pos in positionToOccupy)
        {
            if (placedEntities.ContainsKey(pos))
                throw new Exception($"есть уже такая позиция {pos}");
            placedEntities[pos] = data;
        }
    }
    public int GetEntityIDAt(Vector2Int gridPosition)
    {
        if (placedEntities.ContainsKey(gridPosition) == false)
        {
            return -1;
        }
        else
        {
            return placedEntities[gridPosition].PlacedEntityIndex;
        }
    }
    private List<Vector2Int> CalculatePositions(Vector2Int gridPosition, Vector2Int entitySize)
    {
        List<Vector2Int> val = new();
        for (int x = 0; x < entitySize.x; x++)
        {
            for (int y = 0; y < entitySize.y; y++)
            {
                val.Add(gridPosition + new Vector2Int(x, y));
            }
        }
        return val;
    }
    public bool CanPlaceObjectAt(Vector2Int gridPosition, Vector2Int entitySize)
    {
        List<Vector2Int> positionToOccupy = CalculatePositions(gridPosition, entitySize);
        foreach (Vector2Int pos in positionToOccupy)
        {
            if (placedEntities.ContainsKey(pos)) return false;
        }
        return true;
    }

    public void RemoveObjectAt(Vector2Int gridPos)
    {
        foreach (Vector2Int pos in placedEntities[gridPos].OccupiedPositions)
        {
            placedEntities.Remove(pos);
        }
    }
}
public class PlacementData
{
    public List<Vector2Int> OccupiedPositions;
    public int ID { get; private set; }
    public int PlacedEntityIndex { get; private set; }
    public PlacementData(List<Vector2Int> occupiedPositions, int id, int placedEntityIndex)
    {
        OccupiedPositions = occupiedPositions;
        ID = id;
        PlacedEntityIndex = placedEntityIndex;
    }

}