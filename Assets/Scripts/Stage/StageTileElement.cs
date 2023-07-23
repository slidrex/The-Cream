using Assets.Scripts.Entities;
using Assets.Scripts.Level.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Stage
{
    internal class StageTileElement : MonoBehaviour
    {
        public Transform PlayerPosition;
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
        }
        private void ActivateStaticEntities()
        {
            foreach (var e in _staticEntities) e.HousingElement = this;
        }
    }
}
