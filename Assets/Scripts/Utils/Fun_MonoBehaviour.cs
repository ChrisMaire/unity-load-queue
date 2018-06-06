using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fun_MonoBehaviour : MonoBehaviour {
    private QueueYieldInstruction startQueue = new QueueYieldInstruction();

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
