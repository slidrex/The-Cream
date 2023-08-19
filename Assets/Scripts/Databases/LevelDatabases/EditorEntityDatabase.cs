using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Databases.dto.Runtime;
using Assets.Scripts.Databases.LevelDatabases;
using Assets.Scripts.Databases.dto.Units;

[CreateAssetMenu(fileName = "Editor Database", menuName = "Databases/Create editor database")]
internal class EditorEntityDatabase : ScriptableObject, IEntityDatabase<EditorEntityModel>
{
    [field: SerializeField] public List<EditorEntityModel> Entities { get; set; }
}
