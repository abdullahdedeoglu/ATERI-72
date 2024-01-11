using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuckManGameManager : MonoBehaviour
{
    public Ghosts[] ghosts;
    public Puckman puckman;
    public Transform pellets;
    public Text scoreText;
    public Text livesText;
    public int score {  get; private set; }
    public int lives {  get; private set; }

    public int ghostMultiplier { get; private set; }

    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellets.gameObject.SetActive(true);
        }

        ResetState();
    }
    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.puckman.ResetState();
    }
    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        Debug.Log(lives);
        this.lives = lives;
    }

    private void GameOver()
    {
        GameOverScript.score = this.score;
        LoadGame.sceneName = "PacMan";
        SceneManager.LoadScene("Game Over");
    }

    public void GhostEaten(Ghosts ghost)
    {
        SetScore(this.score + (ghost.point*this.ghostMultiplier));
        this.ghostMultiplier++;
    }

    public void PuckManEaten()
    {
        this.puckman.gameObject.SetActive(false);
        Debug.Log(this.puckman.gameObject.activeSelf);

        SetLives(this.lives-1);

        if (this.lives > 0) Invoke(nameof(ResetState), 2.0f);
        else GameOver();
    }

    public void PellettEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score+pellet.point);

        if (!HasRemainingPellet())
        {
            SceneManager.LoadScene("You Win");
        }
    }

    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        for(int i = 0;i<this.ghosts.Length; i++) this.ghosts[i].frightened.Enable(powerPellet.duration);

        PellettEaten(powerPellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);

    }

    private bool HasRemainingPellet()
    {
        foreach(Transform pellet in this.pellets)
        {
            if(pellet.gameObject.activeSelf) return true;
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
