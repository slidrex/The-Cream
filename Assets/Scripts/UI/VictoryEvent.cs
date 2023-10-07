using UnityEngine;

public class VictoryEvent : MonoBehaviour
{
    private void Start()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerID = SortingLayer.NameToID("Top UI");
    }
}
