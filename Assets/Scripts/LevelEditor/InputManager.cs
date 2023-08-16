using Assets.Editor;
using Assets.Scripts.LevelEditor;
using UnityEngine;
using UnityEngine.EventSystems;

internal class InputManager : MonoBehaviour
{
    [SerializeField] private Sprite _runtimePreviewEntity;
    public bool EnableAlignment { get; set; }
    [field: SerializeField] public PreviewEntity _previewEntity { get; private set; }
    private SpriteRenderer indicatorRenderer;
    private const byte limitingLayer = 7;
    private Vector2Int lastPosition;
    private PreviewManager.PreviewBoundSettings _boundSettings;
    private Grid grid;
    private void Start()
    {
        indicatorRenderer = _previewEntity.GetComponent<SpriteRenderer>();
        grid = Editor.Instance.GetGrid();
    }
    private void Update()
    {
        UpdateCursorPosition();
    }
    public void SetBound(PreviewManager.PreviewBoundSettings settings)
    {
        _boundSettings = settings;
    }
    private void UpdateCursorPosition()
    {
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(GetCursorPosition());
        if (lastPosition == gridPos && Editor.Instance.GameModeIs(GameMode.RUNTIME) == false) return;
        lastPosition = gridPos;
        if(Editor.Instance.GameModeIs(GameMode.EDIT))
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
        UpdatePreviewEntityPosition();
    }
    private void UpdatePreviewEntityFree()
    {
        _previewEntity.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    }
    private Vector2 UpdatePreviewEntityBound()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        Vector2 dist = mousePos - _boundSettings.BoundTransform.position;
        Vector2 resultPos;

        if (dist.sqrMagnitude > _boundSettings.MaxReachDistance * _boundSettings.MaxReachDistance)
        {
            resultPos = _boundSettings.BoundTransform.position + (Vector3)(_boundSettings.MaxReachDistance * dist.normalized);
        }
        else
            resultPos = mousePos;
        _previewEntity.transform.position = resultPos;
        return resultPos;
    }
    private void UpdatePreviewEntityPosition()
    {
        if (Editor.Instance.GameModeIs(GameMode.RUNTIME))
        {
            if (_boundSettings == null) UpdatePreviewEntityFree();
            else UpdatePreviewEntityBound();
        }
        else
        {
            Vector2Int gridPos = (Vector2Int)grid.WorldToCell(GetCursorPosition());
            _previewEntity.transform.position = grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 1)) + new Vector3(_previewEntity.GetModel().Size, _previewEntity.GetModel().Size) / 2;
        }
    }
    public Vector3 GetPreviewEntityPosition()
    {
        if (Editor.Instance.GameModeIs(GameMode.RUNTIME)) return UpdatePreviewEntityBound();
        return _previewEntity.transform.position;
    }
    public void SetPreviewSprite(Sprite sprite)
    {
        _previewEntity.GetRenderer().sprite = sprite;
    }
    public void SetActivePreviewEntity(bool active, Sprite sprite = null, PreviewManager.PreviewBoundSettings settings = null)
    {
        _boundSettings = settings;
        if (Editor.Instance.GameModeIs(GameMode.RUNTIME))
        {
            _previewEntity.GetRenderer().sprite = sprite == null ? _runtimePreviewEntity : sprite;
        }
        else
        {
            _previewEntity.GetRenderer().sprite = sprite == null ? _previewEntity.GetModel().Icon : sprite;
        }
        _previewEntity.gameObject.SetActive(active);
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
