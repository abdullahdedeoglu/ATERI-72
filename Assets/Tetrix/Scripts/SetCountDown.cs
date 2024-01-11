using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCountDown : MonoBehaviour
{
    // Start is called before the first frame update
    private GameStarter gameStarter;
    public void setCountDown()
    {
        gameStarter = GameObject.Find("GameStarter").GetComponent<GameStarter>();
        gameStarter.countDownDone = true;
    }
}
