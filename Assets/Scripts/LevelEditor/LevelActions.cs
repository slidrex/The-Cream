using Assets.Editor;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelActions : MonoBehaviour, IActivateButton
{
    public Button Start, Stop;
    public Action<ButtonType> OnButtonSwitched;
    public void ActivateButton(ButtonType type)
    {
        EnableButton(type);
        OnButtonSwitched.Invoke(type);
        switch (type)
        {
            case ButtonType.START_RUNTIME:
                {
                    break;
                }
            case ButtonType.STOP_RUNTIME:
                {
                    break;
                }
            case ButtonType.MOVE_NEXT_LEVEL:
                {
                    Editor.Instance._spaceController.ClearSpace();
                    break;
                }
            case ButtonType.NONE:
                {
                    break;
                }
        }
    }
    private void EnableButton(ButtonType type)
    {
        Start.gameObject.SetActive(false);
        Stop.gameObject.SetActive(false);

        switch (type)
        {
            case ButtonType.NONE:
                {
                    break;
                }
            case ButtonType.START_RUNTIME:
                {
                    Start.gameObject.SetActive(true);
                    break;
                }
            case ButtonType.STOP_RUNTIME:
                {
                    Stop.gameObject.SetActive(true);
                    break;
                }
            case ButtonType.MOVE_NEXT_LEVEL:
                {
                    break;
                }
        }
    }
}
