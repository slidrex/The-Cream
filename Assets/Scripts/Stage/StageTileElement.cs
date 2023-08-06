using Assets.Scripts.Entities;
using Assets.Scripts.Level.Stages;
using System;
using UnityEngine;

namespace Assets.Scripts.Stage
{
    internal class StageTileElement : MonoBehaviour
    {
        public Transform PlayerPosition;
        [field:SerializeField] public int EditorSpaceRequired { get; set; } = 16;
        public int CameraSize = 5;
        public RelationElement[] Elements;
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
            EnableStaticEntities(false);

		}
        public void EnableStaticEntities(bool active)
        {
            foreach(var e in _staticEntities) e.gameObject.SetActive(active);
        }
        private void ActivateStaticEntities()
        {
            foreach (var e in _staticEntities) e.HousingElement = this;
        }
    }
}
