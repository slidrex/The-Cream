using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Level.Stages
{
    internal class Dockspace : MonoBehaviour
    {
        public enum Direction
        {
            NONE,
            UP,
            DOWN,
            LEFT, 
            RIGHT
        }
        [SerializeField] private Image _cursorImage;
        [SerializeField] private Sprite _arrowSprite;
        [SerializeField] private Direction[] _availableDirections;
        private void Update()
        {

            Direction dir = EnableArrow();
            
            _cursorImage.gameObject.SetActive(dir != Direction.NONE);
            Cursor.visible = dir == Direction.NONE;
            if(dir != Direction.NONE)
            {
                _cursorImage.sprite = _arrowSprite;
                if (dir == Direction.UP) _cursorImage.transform.eulerAngles = Vector3.zero;
                else if (dir == Direction.LEFT) _cursorImage.transform.eulerAngles = Vector3.forward * 90;
                else if (dir == Direction.RIGHT) _cursorImage.transform.eulerAngles = -Vector3.forward * 90;
                else if (dir == Direction.DOWN) _cursorImage.transform.eulerAngles = Vector3.forward * 180;
            }
            Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            _cursorImage.transform.position = pos;
        }
        private Direction EnableArrow()
        {
            Vector2 screenMiddle = new Vector2(Screen.width, Screen.height) / 2;
            Vector2 mouePos = Input.mousePosition;

            bool top = mouePos.y > screenMiddle.y + screenMiddle.y / 2;
            bool down = mouePos.y < screenMiddle.y / 2;
            bool right = mouePos.x > screenMiddle.x + screenMiddle.x / 2;
            bool left = mouePos.x < screenMiddle.x / 2;
            return GetAppropriateDirection(top ? Direction.UP : Direction.NONE, down ? Direction.DOWN : Direction.NONE, left ? Direction.LEFT : Direction.NONE, right ? Direction.RIGHT : Direction.NONE);
        }
        private Direction GetAppropriateDirection(params Direction[] input)
        {
            foreach(var dir in input)
            {
                if(dir != Direction.NONE)
                    foreach(var avDir in _availableDirections)
                    {
                        if(dir == avDir) return dir;
                    }
            }
            return Direction.NONE;
        }
    }
}
