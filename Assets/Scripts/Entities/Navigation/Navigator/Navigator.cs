using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Navigation.Navigator
{
    internal abstract class Navigator : MonoBehaviour
    {
        public abstract void SetTarget(Entity target);
        public abstract Entity GetTarget();
        public abstract Entity GetNearestTarget();
        public abstract bool IsTargetValid(Transform target);
        /// <summary>
        /// Gets target inside view radius.
        /// </summary>
        /// <returns></returns>
        public abstract List<Entity> GetTargets();
    }
}
