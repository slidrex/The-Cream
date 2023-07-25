using Assets.Editor;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Scripts.Databases.dto.Runtime;

internal abstract class ObjectHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image EntityIcon;
    [SerializeField] protected GameObject DescriptionObject;
    [SerializeField] protected TextMeshProUGUI Name, Description, Cost;
    [SerializeField] protected Transform CharacteristicsParent;
    protected ObjectCharacteristic CharacteristicObject;
    protected Button button;
    private const float TIME_TO_APPEAR = 0.5f;
    private float currentTime = 0;
    private bool isCursorInObject = false;
    public virtual void Init(int id, EntityDatabase data, PlacementSystem system)
    {
        EntityIcon.sprite = data.Entities[id].Icon;
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { system.SetCurrentEntityID(id); });
        button.onClick.AddListener(delegate {
            Editor.Instance._inputManager._previewEntity.Init(
                data.Entities[id].Entity.transform.localScale,
                data.Entities[id].Icon,
                data.Entities[id].GetModel());
        });
        ConfigureDescription(id, data);
    }
    protected virtual void ConfigureDescription(int id, EntityDatabase data)
    {
        CharacteristicObject = Resources.Load<ObjectCharacteristic>("UI/ObjectCharacteristic");
        Name.text = data.Entities[id].Description.Name;
        Description.text = data.Entities[id].Description.Description;
        
        for (int i = 0; i < data.Entities[id].Description.Characteristics.Length; i++)
        {
            var charac = Instantiate(CharacteristicObject, CharacteristicsParent);
            charac.Init(GetIcon(data.Entities[id].Description.Characteristics[i].IconType, data, id, i), data.Entities[id].Description.Characteristics[i].Value);
        }
    }
    private Sprite GetIcon(ObjectDescription.IconType iconType, EntityDatabase data, int id, int i)
    {
        switch (iconType)
        {
            case ObjectDescription.IconType.DAMAGE:
                {
                    return CharacteristicIcons.Instance._damageIcon;
                }
            case ObjectDescription.IconType.HEALTH:
                {
                    return CharacteristicIcons.Instance._healthIcon;
                }
            case ObjectDescription.IconType.OTHER:
                {
                    return data.Entities[id].Description.Characteristics[i].CharacteristicIcon;
                }
            default: return null;
        }
    }

    private void Update()
    {
        if(isCursorInObject)
        {
            if (currentTime < TIME_TO_APPEAR)
            {
                currentTime += Time.deltaTime;
            }
            else if(currentTime > TIME_TO_APPEAR && DescriptionObject.activeSelf == false)
            {
                DescriptionObject.SetActive(true);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        currentTime = 0;
        DescriptionObject.SetActive(false);
        isCursorInObject = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentTime = 0;
        isCursorInObject = true;
    }
}