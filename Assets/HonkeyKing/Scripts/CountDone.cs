using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDone : MonoBehaviour
{
    public HKStarter GMS;
    
    public void SetCountdownDone()
    {
        GMS = GameObject.Find("Starter").GetComponent<HKStarter>();
        GMS.counterDown = true;
    }
}
