using Assets.Scripts.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MiniMap
{
    internal class MiniMapElement : MonoBehaviour
    {
        public class MiniMapRelativeObject
        {
            public Direction Direction;
            public MiniMapElement Element;

            public MiniMapRelativeObject(Direction direction, MiniMapElement element)
            {
                Direction = direction;
                Element = element;
            }
        }
        private Image _baseImage;
        [SerializeField] private Color32 _visitedColor;
        [SerializeField] private Image _playerMask;
        private List<MiniMapRelativeObject> _relatives = new();
        private MiniMapController _mapController;
        public enum State
        {
            NONE, PLAYER
        }
        public void AddRelative(MiniMapRelativeObject relative)
        {
            _relatives.Add(relative);
        }
        public void Configure(MiniMapController controller)
        {
            _baseImage = GetComponent<Image>();
            _mapController = controller;
        }
        public void SetState(State state)
        {
            gameObject.SetActive(true);
            if (state == State.PLAYER) _baseImage.color = _visitedColor;
            _playerMask.gameObject.SetActive(state == State.PLAYER);
        }
        public void RevealElement()
        {
            gameObject.SetActive(true);
        }
        public void RevealRelativeElements()
        {
            foreach(var element in _relatives)
            {
                element.Element.RevealElement();
            }
        }
        public MiniMapElement GetCellInDirection(Direction direction)
        {
            foreach(var el in _relatives)
            {
                if(el.Direction == direction)
                {
                    return el.Element;
                }
            }
            return null;
        }
    }
}
