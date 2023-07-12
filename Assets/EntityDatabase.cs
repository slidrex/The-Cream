using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "EntityDatabase", menuName = "Databases/Create entity database")]
public class EntityDatabase : ScriptableObject
{
    public List<EntityData> Entities;
}

[Serializable]
public class EntityData
{
    [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject Prefab { get; private set; }
    public EntityData(Vector2Int size, GameObject prefab)
    {
        Size = size;
        Prefab = prefab;
    }
    
}