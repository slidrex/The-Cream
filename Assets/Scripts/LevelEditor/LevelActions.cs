using Assets.Editor;
using UnityEngine;
using UnityEngine.UI;

public class LevelActions : MonoBehaviour, IActivateButton
{
    public Button Start, Stop, MoveNextLevel;

    public void ActivateButton(ButtonType type)
    {
        EnableButton(type);
        switch (type)
        {
            case ButtonType.START_RUNTIME:
                {
                    Editor.Instance.ClearContent();
                    break;
                }
            case ButtonType.STOP_RUNTIME:
                {
                    Editor.Instance.ClearContent();
                    break;
                }
            case ButtonType.MOVE_NEXT_LEVEL:
                {
                    Editor.Instance.ClearContent();
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
        MoveNextLevel.gameObject.SetActive(false);
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
                    MoveNextLevel.gameObject.SetActive(true);
                    break;
                }
        }
    }
}
