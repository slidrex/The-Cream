using Assets.Scripts.Databases.dto.Runtime;
using UnityEngine;

internal class PreviewEntity : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private EditorEntityModel.Model model;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Init(Vector2 size, Sprite sprite, EditorEntityModel.Model model)
    {
        spriteRenderer.sprite = sprite;
        transform.localScale = size;
        this.model = model;
    }
    public EditorEntityModel.Model GetModel() { return model; }
}
