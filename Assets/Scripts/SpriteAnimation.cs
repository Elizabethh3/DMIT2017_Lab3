using System.Collections;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    bool isPlaying = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private IEnumerator PlayAnimation(AnimationData animation)
    {
        isPlaying = true;
        spriteRenderer.sprite = animation.frames[0];
        int frameCount = animation.frames.Length;
        int frameIndex = 0;

        while (isPlaying)
        {
            yield return new WaitForSeconds(animation.frameDelay);
            frameIndex++;
            if (frameIndex >= frameCount) { frameIndex = 0; }
            spriteRenderer.sprite = animation.frames[frameIndex];

            yield return null;
        }

        yield return null;
    }
    public void StopAnimation() { isPlaying = false; }
    public void InitializeAnimation(AnimationData animation)
    {
        StopAllCoroutines();
        StartCoroutine(PlayAnimation(animation));
    }

}
