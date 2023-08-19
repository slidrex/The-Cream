using Assets.Scripts.Databases.dto.Runtime;
using Assets.Scripts.Databases.dto.Units;
using UnityEngine;

internal class PreviewEntity : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private EntityModel.Model model;
    public SpriteRenderer GetRenderer() { return spriteRenderer; }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Init(Vector2 size, EntityModel.Model model)
    {
        transform.localScale = size;
        this.model = model;
    }
    public EntityModel.Model GetModel() { return model; }
}
