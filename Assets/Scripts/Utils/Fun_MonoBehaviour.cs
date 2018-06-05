using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fun_MonoBehaviour : MonoBehaviour {
    private void Awake()
    {
        Fun_MonoBehaviourInitializer.QueueAwake(this);
    }

    public virtual void AwakeInit()
    {
    }

    private void Start()
    {
        Fun_MonoBehaviourInitializer.QueueStart(this);
    }

    public virtual void StartInit()
    {
    }
}
