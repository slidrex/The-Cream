using UnityEngine;

public class PreviewEntity : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Init(Vector2 size, Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        transform.localScale = size;
    }
}
