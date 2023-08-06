using Assets.Scripts.Config;
using Assets.Scripts.Entities.Util.Config.Input;
using Assets.Scripts.Entities.Util.Config.Menu;
using Assets.Scripts.Menu.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfiguration : MonoBehaviour
{
    private TextMeshProUGUI keyName;
    private Button button;
    private Image buttonImage;
    private TextMeshProUGUI abilityName;
    internal InputConfig.ActionKey ActionKey;
    public KeyCode KeyCode;
    private InputListener listener;
    private InputView inputView;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        buttonImage = button.GetComponent<Image>();
        keyName = button.GetComponentInChildren<TextMeshProUGUI>();
        abilityName = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void StartListenting()
    {
        SetKeyName(KeyCode.None);
        listener.ListenForKey(OnKeyPressed);
    }
    private void OnKeyPressed(KeyCode key)
    {
        if (key == KeyCode.Backspace)
        {
            ConfigManager.Instance.InputConfig.SetKey(ActionKey, KeyCode.None);
            SetKeyName(KeyCode.None);
        }
        else
        {
            SetKeyName(key);
            ConfigManager.Instance.InputConfig.SetKey(ActionKey, key);
            inputView.UpdateConfigurations();
        }
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
        keyName.text = Assets.Scripts.Entities.Util.Config.Input.InputManager.GetKeyName(key);
        KeyCode = key;
    }
}
