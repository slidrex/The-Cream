using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

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
