using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTestScript : Fun_MonoBehaviour {
    public override void AwakeInit()
    {
        Debug.Log("Awakened on frame " + Fun_MonoBehaviourInitializer.Frame);
    }

    protected override IEnumerator Start()
    {
        yield return base.Start();
        Debug.Log("Started on frame " + Fun_MonoBehaviourInitializer.Frame);
    }
}
