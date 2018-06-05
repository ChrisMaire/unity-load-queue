using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fun_UIElement : MonoBehaviour {
    [Header("Display Transition")]
    public UIDisplayTransitionType transitionType;
    public float transitionTime = 0f;

    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;

    protected void DoTransition()
    {
        if(transitionType == UIDisplayTransitionType.None
            || transitionTime == 0)
        {
            return;
        }

        switch(transitionType)
        {
            case UIDisplayTransitionType.Fade:
                StartCoroutine(DoFade(0f, 1f, transitionTime));
                break;
            case UIDisplayTransitionType.Flip:
                StartCoroutine(DoFlip(new Vector3(90, 0, 0), Vector3.zero, transitionTime));
                break;
            default:
                break;
        }
    }

    IEnumerator DoFlip(Vector3 startRotation, Vector3 targetRotation, float flipTime)
    {
        float time = 0f;
        while (time < flipTime)
        {
            time += Time.deltaTime;
            rectTransform.localEulerAngles = Vector3.Lerp(startRotation, targetRotation, (time / flipTime));
            yield return null;
        }
        rectTransform.localEulerAngles = targetRotation;
    }

    IEnumerator DoFade(float startAlpha, float targetAlpha, float fadeTime)
    {
        if(canvasGroup == null)
        {
            canvasGroup = rectTransform.gameObject.AddComponent<CanvasGroup>();
        }

        float time = 0f;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, (time / fadeTime));
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
    }

    protected virtual void Reset()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }
    }
}
