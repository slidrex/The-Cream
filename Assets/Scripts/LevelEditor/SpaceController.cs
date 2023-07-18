using System;
using TMPro;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [SerializeField] private int maxSpaceReqiured;
    [SerializeField] private TextMeshProUGUI spaceRequired;
    public Action<int> OnSpaceChanged { get; set; }
    public int CurrentSpaceReqiured { get; private set; }
    public void SetMaxSpaceReqiured(int required)
    {
        maxSpaceReqiured = required;
    }
    public int GetMaxSpaceReqiured() => maxSpaceReqiured;
    public void ChangeSpace(int required)
    {
        CurrentSpaceReqiured += required;
        if (IsOverloaded(0) == true)
        {
            return;
        }
        else
        {
            spaceRequired.text = CurrentSpaceReqiured.ToString() + "/" + maxSpaceReqiured;
        }
        OnSpaceChanged?.Invoke(CurrentSpaceReqiured);
    }
    public bool IsOverloaded(int space) => CurrentSpaceReqiured + space > maxSpaceReqiured;
}
