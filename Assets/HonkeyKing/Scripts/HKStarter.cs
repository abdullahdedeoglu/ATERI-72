using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKStarter : MonoBehaviour
{

    public bool counterDown = false;

    public GameObject platform;
    public GameObject mario;
    public GameObject honkeyKing;
    public GameObject spawner;
    public GameObject princess;
    public GameObject ladders;
    void Start()
    {
        platform.gameObject.SetActive(false);
        mario.gameObject.SetActive(false);
        honkeyKing.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        princess.gameObject.SetActive(false);
        ladders.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (counterDown)
        {
            platform.gameObject.SetActive(true);
            mario.gameObject.SetActive(true);
            honkeyKing.gameObject.SetActive(true);
            spawner.gameObject.SetActive(true);
            princess.gameObject.SetActive(true);
            ladders.gameObject.SetActive(true);
        }

    }
}
