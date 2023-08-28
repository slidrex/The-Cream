using Assets.Scripts.Entities;
using Assets.Scripts.Functions;
using Assets.Scripts.Level.Stages;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Stage
{
    internal class StageTileElement : MonoBehaviour
    {
        public Transform PlayerPosition;
        public bool IsEmpty;
        [field:SerializeField] public int EditorSpaceRequired { get; set; } = 16;
        public int CameraSize = 5;
        public List<RelationElement> Elements;
        [Header("Element Specific Data")]
        public AudioClip SpecificRuntimeSoundtrack;
        [Serializable]
        public struct RelationElement
        {
            public Direction Direction;
            public StageTileElement Element;
        }
        private Entity[] _staticEntities;
        private void Start()
        {
            _staticEntities = GetComponentsInChildren<Entity>();
            ActivateStaticEntities();
		}
        private void ActivateStaticEntities()
        {
            foreach (var e in _staticEntities) e.HousingElement = this;
        }
        public void TryAddDirection(Direction dir, StageTileElement element)
        {
            foreach(var dirs in Elements)
            {
                if (dirs.Direction == dir) return;
            }
            Elements.Add(new RelationElement() { Direction = dir, Element = element });
        }
    }
}
