using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTestScript : Fun_MonoBehaviour {
    public override void AwakeInit()
    {
        base.AwakeInit();
        Debug.Log("Awakened");
    }

    public override void StartInit()
    {
        base.StartInit();
        Debug.Log("Started");
    }
}
