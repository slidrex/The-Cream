using Assets.Scripts.Level.Stages;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.MiniMap
{
    internal class MiniMapController : MonoBehaviour
    {
        [SerializeField] private float _cellDistance;
        [SerializeField] private MiniMapElement _cell;
        [SerializeField] private Transform _cellContainer;
        [SerializeField] private Transform _initialCellPosition;
        private MiniMapElement _currentElement;
        private void Awake()
        {
            SpawnInitialCell();
            SpawnNextCell(Vector2.up);
            SetCurrentElement(SpawnNextCell(Vector2.right));
            SpawnNextCell(Vector2.up);
            SpawnNextCell(Vector2.right);
        }
        private void SpawnInitialCell()
        {
            _currentElement = SpawnElement(_initialCellPosition.position);
        }
        public void SetCurrentElement(MiniMapElement element)
        {
            _currentElement = element;
        }
        public MiniMapElement SpawnNextCell(Vector2 direction)
        {
            Vector2 newPosition = (Vector2)_currentElement.transform.position + direction * _cellDistance;
            return SpawnElement(newPosition);
        }
        private MiniMapElement SpawnElement(Vector2 position)
        {
            return Instantiate(_cell, position, Quaternion.identity);
        }
    }
}
