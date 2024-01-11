using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceHockeyGameManager : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;

    public Text text;
    public Text text2;

    public Ball ball;
    public Paddle paddle;

    public int brokenBrick;

    public GameObject pauseCanvas;
    private void Start()
    {
        NewGame();
        pauseCanvas.SetActive(false);
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
        this.score = 0;
        this.lives = 3;
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;
        text.text = "Score: " + score;
    }

    public void Miss()
    {
        lives--;
        text2.text = "Lives: " + lives;
        if (lives == 0)
        {
            GameOverScript.score = this.score;
            LoadGame.sceneName = "SpaceHockey";
            SceneManager.LoadScene("Game Over");
        }
        else ResetLevel();
    }

    private void ResetLevel()
    {
       ResetBall();
       ResetPaddle();
    }

    public void ResetBall()
    {
        StartCoroutine(WaitBall());
        ball.SetRandomTrajectoryAfterMiss();
    }

    IEnumerator WaitBall()
    {
        ball.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        ball.gameObject.SetActive(true);
        ball.transform.position = new Vector3(0, -0.5f, 0);
        yield return new WaitForSeconds(1);
    }
    public void ResetPaddle()
    {
        paddle.transform.position = new Vector3(0,-4,0);
    }

    public void WinGame()
    {
        if (brokenBrick == 66) SceneManager.LoadScene("You Win");
    }

}
