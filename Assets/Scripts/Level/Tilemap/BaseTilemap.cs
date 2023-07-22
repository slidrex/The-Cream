using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Level
{
    internal class BaseTilemap : MonoBehaviour
    {
        public UnityEngine.Tilemaps.Tilemap GetMap() => GetComponent<UnityEngine.Tilemaps.Tilemap>();
    }
}
