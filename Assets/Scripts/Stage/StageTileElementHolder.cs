using Assets.Scripts.Databases.LevelDatabases;
using Assets.Scripts.Entities;
using Assets.Scripts.Functions;
using Assets.Scripts.Level;
using Assets.Scripts.Level.Tilemap;
using Assets.Scripts.UI.MiniMap;
using System;
using System.Collections.Generic;
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
        [Header("Element Specific Data")]
        public EditorEntityDatabase SpecificEditorDatabase;
        public RuntimeDatabase SpecificRuntimeDatabase;
        public void Configure()
        {
            InitialElement.IsEmpty = true;
			PlacementTileMap = GetComponentInChildren<BaseTilemap>(true).GetMap();
            LimitingTileMap = GetComponentInChildren<LimitingTilemap>(true).GetMap();
            LinkReducants();

		}
        private HashSet<int> _bannedIndeces = new();
        private MiniMapController _map;
        private Queue<ElementPojo> _elements = new();
        private class ElementPojo
        {
            public StageTileElement el;
            public MiniMapElement MinimapElement;

            public ElementPojo(StageTileElement el, MiniMapElement minimapElement)
            {
                this.el = el;
                MinimapElement = minimapElement;
            }
        }
        public void FillMap()
        {
            _elements = new();
            _bannedIndeces = new();

            _map = FindObjectOfType<MiniMapController>();
            _elements.Enqueue(new(InitialElement, _map.GetCurrentMapElement()));
            _bannedIndeces.Add(InitialElement.GetInstanceID());
            
            while (_elements.TryDequeue(out var currentElement))
            {
                if (currentElement.el != InitialElement) _map.SetCurrentElement(currentElement.MinimapElement);
                foreach (var element in currentElement.el.Elements)
                {
                    if (_bannedIndeces.Contains(element.Element.GetInstanceID()) == false)
                    {
                        var cell = _map.SpawnNextCell(element.Direction);
                        _elements.Enqueue(new (element.Element, cell));
                    }
                }
            }

        }
		private void LinkReducants()
		{
            foreach (var element in GetComponentsInChildren<StageTileElement>())
            {
                foreach(var nest in element.Elements)
                {
                    nest.Element.TryAddDirection(CreamUtilities.GetOppositeDirection(nest.Direction), element);
                }
            }
		}
	}
}
