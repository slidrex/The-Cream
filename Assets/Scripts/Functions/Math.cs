using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Functions
{
    internal static class Mathc
    {
        public static T GetNearestTo<T>(T nearestTo, List<T> objects) where T : MonoBehaviour
        {
            float minDistSqr = 0;
            int findIndex = -1;
            for(int i = 0; i < objects.Count; i++)
            {
                float dist = Vector2.SqrMagnitude(objects[i].transform.position - nearestTo.transform.position);
                if(dist < minDistSqr || findIndex == -1) 
                {
                    findIndex = i;
                    minDistSqr = dist;
                }
            }
            return findIndex == -1 ? null : objects[findIndex];
        }
    }
}
