namespace Assets.Scripts.Entities.Reset
{
    internal interface IResettable
    {
        /// <summary>
        /// Calls when player moves by tile element.
        /// </summary>
        void OnReset();
    }
}
