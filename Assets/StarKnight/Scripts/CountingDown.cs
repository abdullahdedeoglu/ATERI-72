using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingDown : MonoBehaviour
{
    public Starter GMS;

    public void SetCountDownNow()
    {
        GMS = GameObject.Find("GameManager").GetComponent<Starter>();
        GMS.counterDownDone = true;
    }

}
