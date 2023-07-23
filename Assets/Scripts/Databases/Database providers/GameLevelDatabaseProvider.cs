using Assets.Scripts.Databases.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Databases.Database_providers
{
    internal class GameLevelDatabaseProvider : MonoBehaviour
    {
        public static GameLevelDatabaseProvider Instance { get; private set; }
        [field: SerializeField] public CharacterDatabase CharacterDatabase { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
    }
}
