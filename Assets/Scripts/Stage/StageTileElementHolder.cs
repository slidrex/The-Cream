using Assets.Scripts.Entities;
using Assets.Scripts.Level;
using Assets.Scripts.Level.Tilemap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Stage
{
    internal class StageTileElementHolder : MonoBehaviour
    {
        public StageTileElement InitialElement;
        public StageTileElement EndElement;
        public Tilemap PlacementTileMap { get; set; }
        public Tilemap LimitingTileMap { get; set; }
        public void Configure()
        {
            PlacementTileMap = GetComponentInChildren<BaseTilemap>(true).GetMap();
            LimitingTileMap = GetComponentInChildren<LimitingTilemap>(true).GetMap();
        }
    }
}
