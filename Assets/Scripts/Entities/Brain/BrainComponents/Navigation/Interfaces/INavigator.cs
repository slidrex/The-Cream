using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.Navigation.Interfaces
{
    internal interface INavigator
    {
        List<Entity> GetTargets();
        void SetTarget(Transform target);
        Transform GetTargetTransform();
        Entity GetNearestTargetEntity();
        bool IsTargetInsideFindRadius(Transform target);
    }
}
