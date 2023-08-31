namespace Assets.Scripts.Entities.Navigation.Target
{
    internal abstract class TargetModel<T>
    {
        [UnityEngine.SerializeField] private T[] _targets;
        public T[] GetTargets()
        {
            return _targets;
        }
    }
}
