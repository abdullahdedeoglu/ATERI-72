using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public SpaceHockeyStarter GMS;

    public void SetCountDownNow()
    {
        GMS = GameObject.Find("Starter").GetComponent<SpaceHockeyStarter>();
        GMS.counterDown = true;
    }
}
