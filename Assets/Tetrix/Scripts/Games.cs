using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Games : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    public int scoreOneLine = 40;
    public int scoreTwoLine = 100;
    public int scoreThreeLine = 300;
    public int scoreFourLine = 1200;
    private int numberOfRowsThisTurn = 0;

    private AudioSource audioSource;
    public AudioClip clearSound;

    public Text hud_level;
    public Text hud_lines;
    public Text hud_score;
    public static int currentScore=0;

    private GameObject previewTetromino;
    private GameObject nextTetromino;
    private GameObject savedTetromino;
    private bool gameStarted = false;

    public Vector2 previewTetrominoPosition = new Vector2(13f, 16f);
    private Vector2 savedTetrominoPosition = new Vector2(13f, 5f);

    public int maxSwaps = 2;
    private int currentSwaps = 0;

    public int currentLevel = 0;
    private int numLinesCleared = 0;
    public float fallSpeed = 1.0f;

    private GameStarter gameStarter;

    public static bool isPaused = false;

    public Canvas pause_Canvas;
    void Start()
    {
        pause_Canvas.enabled = false;
        gameStarter = GameObject.Find("GameStarter").GetComponent<GameStarter>();
        InvokeRepeating("SpawnNextTetromino", 4.0f, 0);
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {        
        UpdateScore();
        UpdateLevel();
        UpdateSpeed();
        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
                PauseGame();
            else
                ResumeGame();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject tempNextTetromino = GameObject.FindGameObjectWithTag("currentActiveTetromino");
            SaveTetromino(tempNextTetromino.transform);
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        audioSource.Pause();
        isPaused = true;
        pause_Canvas.enabled = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        audioSource.Play();
        isPaused = false;
        pause_Canvas.enabled = false;
    }

    void UpdateLevel()
    {
        currentLevel = numLinesCleared / 10;
        //Debug.Log("current level: "+ currentLevel);
    }
    void UpdateSpeed()
    {
        fallSpeed = 1.0f - ((float)currentLevel * 0.1f);
        //Debug.Log("Current fall speed: " + fallSpeed);
    }
    public void FixedUpdate()
    {
        hud_score.text=currentScore.ToString();
        hud_level.text=currentLevel.ToString();
        //hud_lines.text=numLinesCleared.ToString();
    }
    public void UpdateScore()
    {
        if (numberOfRowsThisTurn >0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                ClearedOneLine();
                PlayCleanAudio();
            }
            else if (numberOfRowsThisTurn == 2)
            {
                ClearedTwoLine();
                PlayCleanAudio();
            }
            else if (numberOfRowsThisTurn == 3)
            {
                ClearedThreeLine();
                PlayCleanAudio();
            }
            else if(numberOfRowsThisTurn == 4)
            {
                ClearedFourLine();
                PlayCleanAudio();
            }
            numberOfRowsThisTurn = 0;
        }
    }

    void PlayCleanAudio()
    {
        
        audioSource.PlayOneShot(clearSound);
        
    }

    public void ClearedOneLine()
    {
        currentScore += scoreOneLine + (currentLevel*20);
        numLinesCleared++;
    }
    public void ClearedTwoLine()
    {
        currentScore += scoreTwoLine + (currentLevel * 25);
        numLinesCleared += 2;
    }
    public void ClearedThreeLine()
    {
        currentScore += scoreThreeLine + (currentLevel * 30);
        numLinesCleared += 3;
    }
    public void ClearedFourLine()
    {
        currentScore += scoreFourLine + (currentLevel * 40);
        numLinesCleared += 4;
    }

    bool CheckIsValidPosition (GameObject tetrominos)
    {
        foreach (Transform mino in tetrominos.transform) { 
            Vector2 pos = Round(mino.position);
            if (!CheckIsInsýdeGrid(pos))
            {
                return false;
            }
            if ((GetTransformGridPosition(pos) != null) && GetTransformGridPosition(pos).parent != tetrominos.transform)  
            {
                return false;
            }
        }
        return true;
    }
    public bool CheckIsAboveGrid(Tetrominos tetrominos)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            foreach (Transform mino in tetrominos.transform)
            {
                Vector2 pos = Round(mino.position);
                if (pos.y > gridHeight - 1)
                {
                    return true;
                }
            } 
        }
        return false;
    }
   
    public bool IsFullRowAt(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x,y]== null)
            {
                return false;
            }
        }
        numberOfRowsThisTurn++;
        return true;
    }

    public void DeleteMinoAt(int y)
    {
        for (int x= 0; x < gridWidth; ++x)
        {
            Destroy(grid[x,y].gameObject);
            grid[x,y] = null;
        }
    }

    public void MoveRowDown(int y)
    {
        for(int x = 0;x < gridWidth; ++x)
        {
            if (grid[x,y] != null) {
                grid[x,y-1] = grid[x,y];
                grid[x,y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public void MoveallRowsDown(int y)
    {
        for (int i=y; i<gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for (int y=0; y<gridHeight; ++y)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveallRowsDown(y+1);
                --y;
            }
        }
    }
    public void UpdateGrid (Tetrominos tetromino)
    {
        for (int y=0; y < gridHeight; ++y)
        {
            for (int x=0; x < gridWidth; ++x)
            {
                //Debug.Log("x: " + x);
                //Debug.Log("y: " + y);
                //Debug.Log("grid: " + grid[x, y]);
                if (grid[x, y] != null)
                {
                    //Debug.Log("Hello");
                    if (grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }
        
        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position);
            //Debug.Log("aa: " + pos);
            //Debug.Log("bb: "+grid[(int)pos.x, (int)pos.y]);
            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
                //Debug.Log("cc " + mino);
                //Debug.Log("dd: "+pos.x + "ee: "+ pos.y);
                //Debug.Log("ff: "+grid[(int)pos.x, (int)pos.y]);
            }
        }
        
    }

    public Transform GetTransformGridPosition (Vector2 pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            //Debug.Log(pos.x);
            //Debug.Log(pos.y);
            //Debug.Log("DD: "+grid[(int)pos.x, (int)pos.y]);
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public void SpawnNextTetromino ()
    {
        //Debug.Log(gameStarter);
        if (gameStarter==true)
        {
            //Debug.Log("are we in?");
            if (!gameStarted)
            {
                gameStarted = true;
                string a = GetRandomTetromino();
                nextTetromino = (GameObject)Instantiate(Resources.Load(a, typeof(GameObject)), new Vector2(5f, 18f), Quaternion.identity);
                string b = GetRandomTetromino();
                previewTetromino = (GameObject)Instantiate(Resources.Load(b, typeof(GameObject)), previewTetrominoPosition, Quaternion.identity);
                previewTetromino.GetComponent<Tetrominos>().enabled = false;
                nextTetromino.tag = "currentActiveTetromino";
            }
            else
            {
                previewTetromino.transform.localPosition = new Vector2(5.0f, 20.0f);
                nextTetromino = previewTetromino;
                nextTetromino.GetComponent<Tetrominos>().enabled = true;
                nextTetromino.tag = "currentActiveTetromino";
                string b = GetRandomTetromino();
                previewTetromino = (GameObject)Instantiate(Resources.Load(b, typeof(GameObject)), previewTetrominoPosition, Quaternion.identity);
                previewTetromino.GetComponent<Tetrominos>().enabled = false;
            }
            currentSwaps = 0;
        }
    }

    public void SaveTetromino(Transform t)
    {
        currentSwaps++;
        if (currentSwaps > maxSwaps)
        {
            return;
        }
        if (savedTetromino != null)
        {
            GameObject tempSavedTetromio = GameObject.FindGameObjectWithTag("currentSavedTetromino");
            tempSavedTetromio.transform.localPosition = new Vector2(gridWidth / 2, gridHeight);

            if (!CheckIsValidPosition(tempSavedTetromio))
            {
                tempSavedTetromio.transform.localPosition = savedTetrominoPosition;
                return;
            }
            savedTetromino = (GameObject)Instantiate(t.gameObject);
            savedTetromino.GetComponent<Tetrominos>().enabled = false;
            savedTetromino.transform.localPosition = savedTetrominoPosition;
            savedTetromino.tag = "currentSavedTetromino";

            nextTetromino = (GameObject)Instantiate(tempSavedTetromio);
            nextTetromino.GetComponent<Tetrominos>().enabled = true;
            nextTetromino.transform.localPosition = new Vector2(gridWidth / 2, gridHeight);
            nextTetromino.tag = "currentActiveTetromino";

            DestroyImmediate(t.gameObject);
            DestroyImmediate(tempSavedTetromio);
        }
        else
        {
            savedTetromino = (GameObject)Instantiate(GameObject.FindGameObjectWithTag("currentActiveTetromino"));
            savedTetromino.GetComponent<Tetrominos>().enabled = false;
            savedTetromino.transform.localPosition= savedTetrominoPosition;
            savedTetromino.tag = "currentSavedTetromino";

            DestroyImmediate(GameObject.FindGameObjectWithTag("currentActiveTetromino"));

            SpawnNextTetromino();
        }
    }
    public bool CheckIsInsýdeGrid (Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round (Vector2 pos)
    {
        return new Vector2 (Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetromino()
    {
        int randomTetromino = Random.Range(0, 8);
        string randomTetrominoName = "IBlock Variant";

        switch (randomTetromino)
        {
            case 1:
                randomTetrominoName = "IBlock Variant";
                break;
            case 2:
                randomTetrominoName = "OBlock Variant";
                break;
            case 3:
                randomTetrominoName = "LBlock Variant";
                break;
            case 4:
                randomTetrominoName = "RLBlock Variant";
                break;
            case 5:
                randomTetrominoName = "ZBlock Variant";
                break;
            case 6:
                randomTetrominoName = "RZBlock Variant";
                break;
            case 7:
                randomTetrominoName = "TBlock Variant";
                break;
        }
        return randomTetrominoName;
    }

    public void GameOver()
    {
        LoadGame.sceneName = "New Scene";
        SceneManager.LoadScene("Game Over");
    }
}
