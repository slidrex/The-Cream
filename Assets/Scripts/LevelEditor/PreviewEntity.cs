using Assets.Scripts.Databases.dto.Runtime;
using UnityEngine;

internal class PreviewEntity : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private EditorEntityModel.Model model;
    public SpriteRenderer GetRenderer() { return spriteRenderer; }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Init(Vector2 size, EditorEntityModel.Model model)
    {
        transform.localScale = size;
        this.model = model;
    }
    public EditorEntityModel.Model GetModel() { return model; }
}
