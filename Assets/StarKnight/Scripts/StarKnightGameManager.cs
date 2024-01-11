using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarKnightGameManager : MonoBehaviour
{
    public int score;

    public GameObject pauseCanvas;

    private void Start()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
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
    public void LooseGame()
    {
        GameOverScript.score = this.score;
        LoadGame.sceneName = "StarKnight";
        SceneManager.LoadScene("Game Over");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("You Win");
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }
}
