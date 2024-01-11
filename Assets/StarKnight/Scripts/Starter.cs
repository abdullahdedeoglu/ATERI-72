using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    public bool counterDownDone = false;

    public GameObject invaders;
    public GameObject shooter;
    public GameObject[] bunkers;
    public GameObject secret;
    public GameObject canvas;

    private void Awake()
    {
        invaders.SetActive(false);
        shooter.SetActive(false);
        secret.SetActive(false);
        bunkers[0].SetActive(false);
        bunkers[1].SetActive(false);
        bunkers[2].SetActive(false);
        bunkers[3].SetActive(false);
    }

    private void Update()
    {
        if (counterDownDone)
        {
            invaders.SetActive(true);
            secret.SetActive(true);
            shooter.SetActive(true);
            bunkers[0].SetActive(true);
            bunkers[1].SetActive(true);
            bunkers[2].SetActive(true);
            bunkers[3].SetActive(true);
        }
    }

}
