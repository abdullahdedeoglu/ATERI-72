using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public string puckManSceneName = "PuckMan";
    public string tetrixSceneName = "New Scene";
    public string honkeyKingSceneName = "HonkeyKing";
    public string spaceHockeySceneName = "SpaceHockey";
    public string starKnightSceneName = "StarKnight";
    public string exitSceneName = "Exit";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Pressed ENTER");
            LoadSelectedScene();
        }
    }

    private void LoadSelectedScene()
    {
        string activeSceneName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(activeSceneName);
        

        switch (activeSceneName)
        {
            case "PuckMan":
                LoadScene(puckManSceneName);
                break;
            case "Tetrix":
                LoadScene(tetrixSceneName);
                break;
            case "HonkeyKing":
                LoadScene(honkeyKingSceneName);
                break;
            case "SpaceHockey":
                LoadScene(spaceHockeySceneName);
                break;
            case "StarKnight":
                LoadScene(starKnightSceneName);
                break;
            case "Exit":
                Application.Quit();
                break;
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
