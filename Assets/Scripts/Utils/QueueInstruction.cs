using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I attempted to use this to control the initialization flow but I couldn't easily sequence them while checking the time
//I think it's possible but need to think on it more
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
