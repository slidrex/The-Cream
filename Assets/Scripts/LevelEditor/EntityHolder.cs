using UnityEngine;
using UnityEngine.UI;

public class EntityHolder : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Init(int id)
    {
        button.onClick.AddListener(delegate { Editor.Instance._placementSystem.SetCurrentEntityID(id); });
    }
}