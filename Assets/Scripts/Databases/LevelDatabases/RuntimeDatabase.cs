using Assets.Scripts.Databases.dto;
using Assets.Scripts.Databases.dto.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Databases.LevelDatabases
{
    [CreateAssetMenu(fileName = "Runtime Database", menuName = "Databases/Create runtime database")]
    internal class RuntimeDatabase : ScriptableObject, IEntityDatabase<RuntimeEntityModel>
    {
        [field: SerializeField] public List<RuntimeEntityModel> Entities { get; set; }
    }
}
