using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "EntityDatabase", menuName = "Databases/Create entity database")]
public class EntityDatabase : ScriptableObject
{
    public List<EntityData> Entities;
    
    public void AddEntity(int id, Vector2Int size, GameObject prefab)
    {
        Entities.Add(new EntityData(id, size, prefab));
    }
}

[Serializable]
public class EntityData
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject Prefab { get; private set; }
    public EntityData(int id, Vector2Int size, GameObject prefab)
    {
        ID = id;
        Size = size;
        Prefab = prefab;
    }
    
}