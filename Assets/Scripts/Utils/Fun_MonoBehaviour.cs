using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fun_MonoBehaviour : MonoBehaviour {
    private QueueYieldInstruction startQueue = new QueueYieldInstruction();

    protected virtual void Awake()
    {
        Fun_MonoBehaviourInitializer.QueueAwake(this);
    }

    public virtual void AwakeInit()
    {
    }

    public void AllowStart()
    {
        startQueue.Waiting = false;
    }

    protected virtual IEnumerator Start()
    {
        Fun_MonoBehaviourInitializer.QueueStart(this);
        yield return startQueue;
    }
}
