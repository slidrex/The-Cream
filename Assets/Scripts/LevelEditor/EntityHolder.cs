using UnityEngine;
using UnityEngine.UI;

public class EntityHolder : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Button button;

    public void Init(int id, EntityDatabase data, PlacementSystem system)
    {
        icon.sprite = data.Entities[id].Prefab.GetComponent<SpriteRenderer>().sprite;
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { system.SetCurrentEntityID(id); });
        button.onClick.AddListener(delegate {
            Editor.Instance._inputManager._previewEntity.Init(
                data.Entities[id].Prefab.transform.localScale, 
                data.Entities[id].Prefab.GetComponent<SpriteRenderer>().sprite);
        });
    }
}