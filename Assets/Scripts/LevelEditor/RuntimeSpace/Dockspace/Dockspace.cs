using Assets.Scripts.Functions;
using Assets.Scripts.Sound;
using Assets.Scripts.Stage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Level.Stages
{
    internal class Dockspace : MonoBehaviour
    {
        [SerializeField] private Image _cursorImage;
        [SerializeField] private Sprite _arrowSprite;
        private Direction[] _availableDirections;
        private Direction _currentDirection;
        private bool _isActive;
        private StageTileElement _currentElement;
        private StageController _stageController;
        private StageTileElement[] _tileElements;
        private const float DOCKSPACE_SCREEN_ACTIVE_INTENSITY = 2;
        private void Start()
        {
            _tileElements = FindObjectsOfType<StageTileElement>();
            _cursorImage.sprite = _arrowSprite;
        }
        private void Update()
        {
            if (_isActive) OnDockspaceActive();
        }
        public void EnableDockspace(Direction[] availableDirections, StageController stageController)
        {
            _availableDirections = availableDirections;
            _isActive = true;
            _stageController = stageController;
        }
        public void Move()
        {
			print("dockspace move");
			_stageController.Move(_currentDirection);
        }
        public void DisableDockspace()
        {
            _isActive = false;
            Cursor.visible = true;
            _currentDirection = Direction.NONE;
            _cursorImage.gameObject.SetActive(false);
        }
        private void OnDockspaceActive()
        {
            _currentDirection = EnableArrow();

            _cursorImage.gameObject.SetActive(_currentDirection != Direction.NONE);
            Cursor.visible = _currentDirection == Direction.NONE;
            if (_currentDirection != Direction.NONE)
            {
                if (_currentDirection == Direction.UP) _cursorImage.transform.eulerAngles = Vector3.zero;
                else if (_currentDirection == Direction.LEFT) _cursorImage.transform.eulerAngles = Vector3.forward * 90;
                else if (_currentDirection == Direction.RIGHT) _cursorImage.transform.eulerAngles = -Vector3.forward * 90;
                else if (_currentDirection == Direction.DOWN) _cursorImage.transform.eulerAngles = Vector3.forward * 180;
            }
            Vector2 pos = Editor.Editor.Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            _cursorImage.transform.position = pos;

        }
        private Direction EnableArrow()
        {
            Vector2 screenMiddle = new Vector2(Screen.width, Screen.height) / 2;
            Vector2 mouePos = Input.mousePosition;

            bool top = mouePos.y > screenMiddle.y + screenMiddle.y / DOCKSPACE_SCREEN_ACTIVE_INTENSITY;
            bool down = mouePos.y < screenMiddle.y / DOCKSPACE_SCREEN_ACTIVE_INTENSITY;
            bool right = mouePos.x > screenMiddle.x + screenMiddle.x / DOCKSPACE_SCREEN_ACTIVE_INTENSITY;
            bool left = mouePos.x < screenMiddle.x / DOCKSPACE_SCREEN_ACTIVE_INTENSITY;
            return GetAppropriateDirection(top ? Direction.UP : Direction.NONE, down ? Direction.DOWN : Direction.NONE, left ? Direction.LEFT : Direction.NONE, right ? Direction.RIGHT : Direction.NONE);
        }
        private Direction GetAppropriateDirection(params Direction[] input)
        {
            foreach (var dir in input)
            {
                if (dir != Direction.NONE)
                    foreach (var avDir in _availableDirections)
                    {
                        if (dir == avDir) return dir;
                    }
            }
            return Direction.NONE;
        }
    }
}
