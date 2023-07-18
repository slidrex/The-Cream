using Assets.Editor;
using UnityEngine;
using UnityEngine.UI;

internal class EntityHolder : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Button button;

    public void Init(int id, EntityDatabase data, PlacementSystem system)
    {
        icon.sprite = data.Entities[id].Icon;
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { system.SetCurrentEntityID(id); });
        button.onClick.AddListener(delegate {
            Editor.Instance._inputManager._previewEntity.Init(
                data.Entities[id].Entity.transform.localScale,
                data.Entities[id].Icon,
                data.Entities[id].GetModel());
        });
    }
}