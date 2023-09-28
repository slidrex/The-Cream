using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using System;
using TMPro;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    private int maxSpaceReqiured;
    [SerializeField] private TextMeshProUGUI spaceRequired;
    public Action<int> OnSpaceChanged { get; set; }
    public int CurrentSpaceReqiured { get; private set; }
	private void Awake()
	{
        LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnModeChanged;
	}
    private void OnDestroy()
    {
		LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnModeChanged;
	}
    private void OnModeChanged(GameMode mode)
    {
        spaceRequired.gameObject.SetActive(mode == GameMode.EDIT);
    }
	public void SetMaxSpaceReqiured(int required)
    {
        maxSpaceReqiured = required;
        spaceRequired.text = CurrentSpaceReqiured.ToString() + "/" + maxSpaceReqiured;
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
            OnSpaceChanged?.Invoke(CurrentSpaceReqiured);
            spaceRequired.color = Color.white;
        }

        if (IsOne() == true)
        {
            spaceRequired.color = Color.red;
        }
    }
    public void ClearSpace()
    {
        CurrentSpaceReqiured = 0;
        spaceRequired.text = CurrentSpaceReqiured.ToString() + "/" + maxSpaceReqiured;
    }

    public bool IsOverloaded(int space)
    {
        return CurrentSpaceReqiured + space > GetMaxSpaceReqiured() + 1;
    }
    private bool IsOne()
    {
        return CurrentSpaceReqiured == GetMaxSpaceReqiured() + 1;
    }
}
