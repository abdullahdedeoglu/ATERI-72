using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHockeyStarter : MonoBehaviour
{
    public bool counterDown = false;

    public GameObject paddle;
    public GameObject ball;
    public GameObject breaks;
    void Start()
    {
        paddle.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);
        breaks.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (counterDown)
        {
            paddle.gameObject.SetActive(true);
            ball.gameObject.SetActive(true);
            breaks.gameObject.SetActive(true);
        }
    }
}
