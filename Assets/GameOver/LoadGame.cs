using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public static string sceneName;
    public string mainMenuSceneName = "MainMenu";
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadSelectedScene();
        }
    }

    private void LoadSelectedScene()
    {
        string activeSceneName = EventSystem.current.currentSelectedGameObject.name;

        switch (activeSceneName)
        {
            case "tryAgain":
                LoadScene(sceneName);
                break;
            case "backToMainMenu":
                LoadScene(mainMenuSceneName);
                break;
        }
    }
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
