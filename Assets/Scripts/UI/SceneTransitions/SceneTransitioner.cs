using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneTransitioner : MonoBehaviour
{
    public UnityEvent OnTransitionEnd = new UnityEvent();

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public virtual void Run(SceneTransitionDirection direction, float time, bool destroyOnComplete)
    {
        OnTransitionEnd.Invoke();
        if(destroyOnComplete)
        {
            Destroy(gameObject);
        }
    }

    public static SceneTransitioner GetTransition(SceneTransitionType transitionType)
    {
        string path = "";

        switch (transitionType)
        {
            case SceneTransitionType.Fade:
                path = "Prefabs/UI/PR_FadeTransition";
                break;
            default:
                path = "Prefabs/UI/PR_FadeTransition";
                break;
        }

        SceneTransitioner transitioner = null;
        if (!string.IsNullOrEmpty(path))
        {
            GameObject transitionerGO = GameObject.Instantiate(Resources.Load(path) as GameObject);
            transitioner = transitionerGO.GetComponent<SceneTransitioner>();
        }
        return transitioner;
    }
}
