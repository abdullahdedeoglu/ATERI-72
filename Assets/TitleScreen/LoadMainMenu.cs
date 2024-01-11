using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
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
        if (Input.anyKey)
        {
            Debug.Log("Pressed something");
            LoadScreen(sceneName);
        }
    }

    private void LoadScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //SceneManager.UnloadSceneAsync(previous);
        
    }
}
