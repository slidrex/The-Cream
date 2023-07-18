using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Databases.dto.Runtime;

[CreateAssetMenu(fileName = "EntityDatabase", menuName = "Databases/Create entity database")]
internal class EntityDatabase : ScriptableObject
{
    public List<EditorEntityModel> Entities;
}
