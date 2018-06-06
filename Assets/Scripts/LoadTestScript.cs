using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTestScript : Fun_MonoBehaviour {
    protected void Awake()
    {
        Debug.Log("Awakened");
    }

    protected override IEnumerator Start()
    {
        yield return base.Start();
        Debug.Log("Started");
    }
}
