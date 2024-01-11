using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int lives;
    private int score=0;

    public GameObject pauseCanvas;

    public string sceneName;

    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1) PauseGame();
            else ResumeGame();
        }

        if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Pressed esc");
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);

    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    private void NewGame()
    {
        lives = 3;
        score = 0;

        //Load Level
    }

    public void LevelComplete()
    {
        score += 1000;
        SceneManager.LoadScene(sceneName);

    }

    public void LevelFailed()
    {
        lives--;

        if (lives<= 0)
        {
            LoadGame.sceneName = "HonkeyKing";
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            //Reload current level
        }
    }

}
