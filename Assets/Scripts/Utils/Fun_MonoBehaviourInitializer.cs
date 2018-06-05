using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Should this actually be a monobehaviour? If we weren't using the level loader this'd be important
/// </summary>
public class Fun_MonoBehaviourInitializer : MonoBehaviour {
    //if this is true, the awakes/starts will run in this monobehaviours natural start/awake. If not they need to be called manually.
    public static bool ManualInitialization = true;
    public static bool Initialized = false;

    public UnityEvent OnInitializationComplete = new UnityEvent();

    private static List<Fun_MonoBehaviour> awakeQueue = new List<Fun_MonoBehaviour>();
    private static List<Fun_MonoBehaviour> startQueue = new List<Fun_MonoBehaviour>();

    //if we go over this time running awake/starts, we'll move the rest to the next frame
    private const float maxFrameTime = 0.033f;

    private void Awake()
    {
        Initialized = false;

        if(!ManualInitialization)
        {
            StartCoroutine(DoAllAwakes());
        }
    }

    private IEnumerator DoAllAwakes()
    {
        while (awakeQueue.Count > 0)
        {
            if(awakeQueue[0] != null)
            {
                awakeQueue[0].AwakeInit();
            }
            awakeQueue.RemoveAt(0);

            if (Time.unscaledDeltaTime > maxFrameTime)
            {
                yield return null;
            }
        }
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
        while(awakeQueue.Count > 0)
        {
            yield return null;
        }

        while (startQueue.Count > 0)
        {
            if(startQueue[0] != null)
            {
                startQueue[0].StartInit();
            }
            startQueue.RemoveAt(0);

            if (Time.unscaledDeltaTime > maxFrameTime)
            {
                yield return null;
            }
        }

        OnInitializationComplete.Invoke();
        Initialized = true;
    }

    public static void Run()
    {
        GameObject initializerGO = new GameObject("MonoBehavior Initializer");
        Fun_MonoBehaviourInitializer initializer = initializerGO.AddComponent<Fun_MonoBehaviourInitializer>();
        initializer.RunInit();
    }

    public void RunInit()
    {
        Initialized = false;
        StartCoroutine(DoAllAwakes());
        StartCoroutine(DoAllStarts());
    }

    public static void QueueAwake(Fun_MonoBehaviour newBehaviour)
    {
        if(!Initialized)
        {
            awakeQueue.Add(newBehaviour);
        } else
        {
            newBehaviour.AwakeInit();
        }
    }

    public static void QueueStart(Fun_MonoBehaviour newBehaviour)
    {
        if (!Initialized)
        {
            startQueue.Add(newBehaviour);
        }
        else
        {
            newBehaviour.StartInit();
        }
    }
}
