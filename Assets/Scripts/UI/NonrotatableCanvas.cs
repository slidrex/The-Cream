using Assets.Scripts.Entities.AI.SightStalking;
using UnityEngine;

public class NonrotatableCanvas : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

