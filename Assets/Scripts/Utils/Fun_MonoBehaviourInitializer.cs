using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Should this actually be a monobehaviour? If we weren't using the level loader this'd be important
/// </summary>
public class Fun_MonoBehaviourInitializer : MonoBehaviour {
    //if this is true, the awakes/starts will run in this monobehaviours natural start/awake. If not they need to be called manually.
    public static bool ManualInitialization = true;
    public static bool Initialized = false;

    public UnityEvent OnInitializationComplete = new UnityEvent();

    private static List<Fun_MonoBehaviour> startQueue = new List<Fun_MonoBehaviour>();

    //if we go over this time running awake/starts, we'll move the rest to the next frame
    private const float maxFrameTime = 0.033f;

    private void Awake()
    {
        Initialized = false;
    }

    private void Start()
    {
        if (!ManualInitialization)
        {
            StartCoroutine(DoAllStarts());
        }
    }

    private IEnumerator DoAllStarts()
    {
        System.DateTime startTime = System.DateTime.Now;
        while (startQueue.Count > 0)
        {
            if(startQueue[0] != null)
            {
                startQueue[0].AllowStart();
            }
            startQueue.RemoveAt(0);

            if ((System.DateTime.Now - startTime).TotalSeconds > maxFrameTime)
            {
                startTime = System.DateTime.Now;
                yield return null;
            }
        }

        OnInitializationComplete.Invoke();
        Initialized = true;
    }

    public static Fun_MonoBehaviourInitializer Run()
    {
        GameObject initializerGO = new GameObject("MonoBehavior Initializer");
        Fun_MonoBehaviourInitializer initializer = initializerGO.AddComponent<Fun_MonoBehaviourInitializer>();
        initializer.RunInit();
        return initializer;
    }

    public void RunInit()
    {
        Initialized = false;
        StartCoroutine(DoAllStarts());
    }

    public static void QueueStart(Fun_MonoBehaviour newBehaviour)
    {
        if (!Initialized)
        {
            startQueue.Add(newBehaviour);
        }
        else
        {
            newBehaviour.AllowStart();
        }
    }
}

#if UNITY_EDITOR
[InitializeOnLoadAttribute]
public static class InitializeInEditor
{
    static InitializeInEditor()
    {
        EditorApplication.playModeStateChanged += HandlePlayModeChanged;
    }

    private static void HandlePlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            Time.timeScale = 0f;
            Fun_MonoBehaviourInitializer initializer = Fun_MonoBehaviourInitializer.Run();
            initializer.OnInitializationComplete.AddListener(LoadEnded);
            if (Fun_MonoBehaviourInitializer.Initialized)
            {
                LoadEnded();
            }
        }
    }

    static void LoadEnded()
    {
        Time.timeScale = 1f;
    }
}
#endif