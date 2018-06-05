using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float speed = 1f;
	void Start () {
        StartCoroutine(DoRotate());
	}

    IEnumerator DoRotate()
    {
        while(true)
        {
            transform.Rotate(0, 0, speed * Time.unscaledDeltaTime);
            yield return null;
        }
    }
}
