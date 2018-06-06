using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueYieldInstruction : CustomYieldInstruction
{
    public bool Waiting = true;
    public override bool keepWaiting
    {
        get
        {
            return Waiting;
        }
    }
}
