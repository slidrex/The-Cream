namespace Assets.Editor
{
    public enum ButtonType
    {
        NONE,
        START_RUNTIME,
        STOP_RUNTIME,
        MOVE_NEXT_LEVEL
    }
    internal interface IActivateButton
    {
        void ActivateButton(ButtonType type);
    }
}