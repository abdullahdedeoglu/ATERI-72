using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCounterDown : MonoBehaviour
{
    public PGameStarter GMS;

    public void SetCountDownNow()
    {
        GMS = GameObject.Find("Starter").GetComponent<PGameStarter>();
        GMS.counterDown = true;
    }
}
