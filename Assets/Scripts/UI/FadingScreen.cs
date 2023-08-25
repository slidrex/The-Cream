using System.Collections;
using UnityEngine;

public class FadingScreen : MonoBehaviour
{
    private const string FADEOUT_TRIGGER = "FadeOut";
    private const string FADEIN_TRIGGER = "FadeIn";
    private Animator animator;
    // 0 - fade in, 1 - fadeOut
    private AnimationClip[] clips;
    public float _fadeInLength {  get; private set; }
    public float _fadeOutLength { get; private set; }
    private void Start()
    {
        animator = GetComponent<Animator>();
        clips = animator.runtimeAnimatorController.animationClips;
        _fadeInLength = clips[0].length;
        _fadeOutLength = clips[1].length;
    }
    public void FadeOut()
    {
        animator.SetTrigger(FADEOUT_TRIGGER);
    }
    public void FadeIn()
    {
        animator.SetTrigger(FADEIN_TRIGGER);
    }
    public void FadeInAndOut()
    {
        StartCoroutine(Glimmer());
    }
    private IEnumerator Glimmer()
    {
        animator.SetTrigger(FADEIN_TRIGGER);
        yield return new WaitForSeconds(_fadeInLength);
        animator.SetTrigger(FADEOUT_TRIGGER);
    }
}
