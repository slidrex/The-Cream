using Assets.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Entities.Player;
using System;

internal abstract class ObjectHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image EntityIcon;
    [SerializeField] protected GameObject DescriptionObject;
    [SerializeField] protected TextMeshProUGUI Name, Description, Cost;
    [SerializeField] protected Transform CharacteristicsParent;
    [SerializeField] private GameObject _keyIcon;
    [SerializeField] private TextMeshProUGUI _bindedKey;
    protected ObjectCharacteristic[] CharacteristicObjects;
    protected Button button;
    private const float TIME_TO_APPEAR = 0.4f;
    private float currentTime = 0;
    private bool isCursorInObject = false;
    public virtual void Init(int id, EntityDatabase data, PlacementSystem system, KeyCode bindedKey)
    {
        EntityIcon.sprite = data.Entities[id].Icon;
        button = GetComponent<Button>();
        UnityEngine.Events.UnityAction firstAct = () => system.SetCurrentEntityID(id);
        UnityEngine.Events.UnityAction secondAct = () =>
        {
            Editor.Instance._inputManager._previewEntity.Init(
                data.Entities[id].Entity.transform.localScale,
                data.Entities[id].GetModel());
        };
        button.onClick.AddListener(firstAct);
        button.onClick.AddListener(secondAct);
        SetBindedKey(bindedKey);
        if(bindedKey != KeyCode.None)
        Assets.Scripts.Entities.Util.Config.Input.InputManager.Bind(bindedKey,() => InvokeActs(firstAct, secondAct));
        ConfigureDescription(id, data);
    }
    private void InvokeActs(UnityEngine.Events.UnityAction first, UnityEngine.Events.UnityAction second)
    {
        first.Invoke();
        second.Invoke();
    }
    public void SetBindedKey(KeyCode key, bool showIcon = true)
    {
        if(_keyIcon != null)
        _keyIcon.gameObject.SetActive(showIcon);
        if(_bindedKey != null)
        _bindedKey.text = Assets.Scripts.Entities.Util.Config.Input.InputManager.GetKeyName(key);
    }
    protected virtual void ConfigureDescription(int id, EntityDatabase data)
    {
        CharacteristicObjects = CharacteristicsParent.GetComponentsInChildren<ObjectCharacteristic>();
        for (int i = 0; i < CharacteristicObjects.Length; i++)
            CharacteristicObjects[i].gameObject.SetActive(false);

        Name.text = data.Entities[id].Description.Name;
        Description.text = data.Entities[id].Description.Description;
        
        for (int i = 0; i < data.Entities[id].Description.Characteristics.Length; i++)
        {
            CharacteristicObjects[i].gameObject.SetActive(true);
            CharacteristicObjects[i].Init(GetIcon(data.Entities[id].Description.Characteristics[i].IconType, data, id, i), data.Entities[id].Description.Characteristics[i].Value);
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
                StartCoroutine(LayoutUpdater());
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
    public IEnumerator LayoutUpdater()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(DescriptionObject.GetComponent<RectTransform>());
    }
}