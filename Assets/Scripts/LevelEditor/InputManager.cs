using Assets.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

internal class InputManager : MonoBehaviour
{
    [SerializeField] private Transform cellIndicator;
    [field: SerializeField] public PreviewEntity _previewEntity { get; private set; }
    private SpriteRenderer indicatorRenderer;
    private const byte limitingLayer = 7;
    private Vector2Int lastPosition;
    private Grid grid;
    private void Start()
    {
        indicatorRenderer = cellIndicator.GetComponent<SpriteRenderer>();
        grid = Editor.Instance.GetGrid();
    }
    private void Update()
    {
        UpdateCursorPosition();
    }
    private void UpdateCursorPosition()
    {
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(GetCursorPosition());
        if (lastPosition == gridPos) return;
        lastPosition = gridPos;
        if (_previewEntity.GetModel().Entity != null)
        {
            cellIndicator.position = grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 0)) + new Vector3(_previewEntity.GetModel().Size, _previewEntity.GetModel().Size)/2;

        }
        if(Editor.Instance.GameModeIs(Editor.GameMode.EDIT))
        {
            if (Editor.Instance._editSystem.GetSelectedEntityIndex() < 0) return;
            bool placementValidity = Editor.Instance._editSystem.CheckPlacementValidity(gridPos, Editor.Instance._editSystem.GetSelectedEntityIndex());
            if (placementValidity == true)
            {
                ColorIndicator(new Color32(0, 255, 0, 100));
            }
            else
            {
                ColorIndicator(new Color32(255, 0, 0, 100));
            }
        }
        if (Editor.Instance.GameModeIs(Editor.GameMode.RUNTIME))
        {
            if (Editor.Instance._runtimeSystem.GetSelectedEntityIndex() < 0) return;
            bool placementValidity = Editor.Instance._runtimeSystem.CheckPlacementValidity(gridPos, Editor.Instance._runtimeSystem.GetSelectedEntityIndex());
            if (placementValidity == true)
            {
                ColorIndicator(new Color32(0, 255, 0, 100));
            }
            else
            {
                ColorIndicator(new Color32(255, 0, 0, 100));
            }
        }
    }
    public Vector2 GetCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && hit.collider.gameObject.layer != limitingLayer)
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
            return new Vector2(-10, 20);
    }
    private void ColorIndicator(Color32 color)
    {
        indicatorRenderer.color = color;
    }
    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
