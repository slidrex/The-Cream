using Assets.Scripts.Entities.Util.Config.Input;
using Assets.Scripts.Entities.Util.Config.Menu;
using Assets.Scripts.Menu.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfiguration : MonoBehaviour
{
    private TextMeshProUGUI keyName;
    private Button key;
    private TextMeshProUGUI abilityName;
    internal InputConfig.ActionKey ActionKey;
    public KeyCode KeyCode;
    private InputListener listener;
    private InputView inputView;

    private void Awake()
    {
        key = GetComponentInChildren<Button>();
        keyName = key.GetComponentInChildren<TextMeshProUGUI>();
        abilityName = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void StartListenting()
    {
        SetKeyName(KeyCode.None);
        listener.ListenForKey(OnKeyPressed);
    }
    private void OnKeyPressed(KeyCode key)
    {
        SetKeyName(key);
        InputConfig.SetKey(ActionKey, key);
        inputView.UpdateConfigurations();
    }
    internal void SetActionKey(InputConfig.ActionKey key)
    {
        ActionKey = key;
    }
    internal void SetListener(InputListener l)
    {
        listener = l;
    }
    internal void SetInputView(InputView view)
    {
        inputView = view;
    }
    public void SetAbilityName(string name)
    {
        abilityName.text = name;
    }
    public void SetKeyName(KeyCode key)
    {
        keyName.text = key.ToString();
        if (key == KeyCode.None)
            keyName.text = "_";
        KeyCode = key;
    }
}
