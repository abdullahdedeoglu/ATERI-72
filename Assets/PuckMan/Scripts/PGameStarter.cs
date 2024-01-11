using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGameStarter : MonoBehaviour
{
    public bool counterDown = false;

    public GameObject gameManager;
    public GameObject puckman;
    public GameObject inky;
    public GameObject blinky;
    public GameObject clyde;
    public GameObject pinky;


    void Start()
    {
        gameManager.gameObject.SetActive(false);
        puckman.gameObject.SetActive(false);
        inky.gameObject.SetActive(false);
        blinky.gameObject.SetActive(false);
        pinky.gameObject.SetActive(false);
        clyde.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (counterDown)
        {
            gameManager.gameObject.SetActive(true);
            puckman.gameObject.SetActive(true);
            inky.gameObject.SetActive(true);
            blinky.gameObject.SetActive(true);
            pinky.gameObject.SetActive(true);
            clyde.gameObject.SetActive(true);
        }
    }
}
