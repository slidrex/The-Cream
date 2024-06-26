using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCharacteristic : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI value;

    public void Init(Sprite icon, string value)
    {
        this.icon.sprite = icon;
        this.value.text = value;
    }

    public Sprite GetIcon() => icon.sprite;
    public string GetValue() => value.text;
}
