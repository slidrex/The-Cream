using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Transform cellIndicator;
    private Grid grid;
    private void Start()
    {
        grid = Editor.Instance._placementSystem.GetGrid();
    }
    private void Update()
    {
        UpdateCursorPosition();
    }
    private void UpdateCursorPosition()
    {
        if (Editor.Instance._placementSystem.GetSelectedEntityIndex() < 0) return;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(GetCursorPosition());
        cellIndicator.position = grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 0));
    }
    public Vector2 GetCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
            return Vector2.zero;
    }
    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
