using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransition : SceneTransitioner {
    public CanvasGroup canvasGroup;

    public override void Run(SceneTransitionDirection direction, float time, bool destroyOnComplete)
    {
        float start = 0;
        float end = 1;
        if(direction == SceneTransitionDirection.In)
        {
            start = 1;
            end = 0;
        }
        Debug.Log("Starting fade : " + direction);
        StopAllCoroutines();
        StartCoroutine(DoFade(time, start, end, destroyOnComplete));
    }

    public IEnumerator DoFade(float fadeTime, float startAlpha, float endAlpha, bool destroyOnComplete)
    {
        float t = 0;
        while(t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, (t / fadeTime));
            yield return null;
        }
        canvasGroup.alpha = endAlpha;

        OnTransitionEnd.Invoke();
        if(destroyOnComplete)
        {
            Destroy(gameObject);
        }
    }
}
