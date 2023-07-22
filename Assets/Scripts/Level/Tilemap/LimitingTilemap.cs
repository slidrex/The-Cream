using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Level.Tilemap
{
    internal class LimitingTilemap : MonoBehaviour
    {
        public UnityEngine.Tilemaps.Tilemap GetMap() => GetComponent<UnityEngine.Tilemaps.Tilemap>();
    }
}
