using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite activeState, passiveState;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(activeState != null)
            image.sprite = activeState;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(passiveState != null)
            image.sprite = passiveState;
    }
}
