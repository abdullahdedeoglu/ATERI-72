using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvasLoad : MonoBehaviour
{
    public string sceneName = "MainMenu";
    //public string previous = "IntroScreen";

    //private void Start()
    //{
    //    //ss
    //}

    private void Update()
    {
        //Debug.Log("In the zone");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Pressed something");
            LoadScreen(sceneName);
        }
    }

    private void LoadScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //SceneManager.UnloadSceneAsync(previous);

    }
}
