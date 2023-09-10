using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Functions;
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
        private List<MiniMapElement> _elements;
        private MiniMapElement _currentElement;

        public MiniMapElement GetCurrentMapElement() => _currentElement;
        public void Configure()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnGameModeChanged;
        }
        public void Unconfigure()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnGameModeChanged;
        }
        private void OnGameModeChanged(GameMode mode)
        {
            _cellContainer.gameObject.SetActive(mode == GameMode.EDIT || mode == GameMode.NONE);
        }
        private void SpawnInitialCell()
        {
            SetCurrentElement(SpawnElement(_initialCellPosition.position, false));
        }
        public void SetCurrentElement(MiniMapElement element)
        {
            _currentElement = element;
        }
        public MiniMapElement SpawnNextCell(Direction direction)
        {
            Vector2 newPosition = (Vector2)_currentElement.transform.position + CreamUtilities.GetDirectionVector(direction) * _cellDistance;
            var element =  SpawnElement(newPosition);
            _currentElement.AddRelative(new MiniMapElement.MiniMapRelativeObject(direction, element));
            element.AddRelative(new MiniMapElement.MiniMapRelativeObject(CreamUtilities.GetOppositeDirection(direction), _currentElement));
            return element;
        }
        private MiniMapElement SpawnElement(Vector2 position, bool hide = true)
        {
            var cell = Instantiate(_cell, position, Quaternion.identity, _cellContainer);
            cell.Configure(this);
            cell.transform.localPosition = new(cell.transform.localPosition.x, cell.transform.localPosition.y, 0);
            _elements.Add(cell);
            if (hide) cell.gameObject.SetActive(false);
            return cell;
        }
        public void ResetMap()
        {
            print("reset map");
            if(_elements != null)
                foreach(var el in _elements)
                {
                    Destroy(el.gameObject);
                }
            _elements = new();
            SpawnInitialCell();
        }
    }
}
