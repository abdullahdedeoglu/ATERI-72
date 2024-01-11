using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows=5;
    public int columns=11;

    public float speed = 0.5f;
    private Vector3 _direction = Vector2.right;

    Vector3 pausedPosition;
    private bool isPaused=false;

    public float customDeltaTÝme = 1.0f / 720.0f;

    public int amountKilled {  get; private set; }
    public int amountAlive => totalInvaders - amountKilled;
    public int totalInvaders => rows * columns;
    public float percentKilled => (float)amountKilled / (float)totalInvaders;

    public float missileAttackRate = 1.0f;

    public Projectile missilePrefab;

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);
    }

    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy) continue;
            float randomValue = Random.value;



            if (randomValue < (1.0f/(float)amountAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
    private void Awake()
    {
        for (int row=0; row<this.rows; row++)
        {
            Vector3 rowPosition = new Vector3(-5.0f,row*1.0f+1.0f ,0.0f);
            for (int col=0; col<this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 1.0f;
                invader.transform.position = position;
            }
        }
    }

    private void Update()
    {
        CheckPause();
        this.transform.position += _direction * this.speed * customDeltaTÝme;

        float leftEdge = -0.5f;
        float rightEdge = 0.5f;

        if (this.transform.position.x >= rightEdge)
        {
            AdvanceRow();
        }
        else if (this.transform.position.x <= leftEdge)
        {
            AdvanceRow();
        }
    }
    public void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                isPaused = true;
                pausedPosition = this.transform.position;
                this.transform.position = pausedPosition;
                speed = 0;
            }
            else
            {
                isPaused= false;
                speed=0.5f;
                this.transform.position=pausedPosition;
            }
        }
    }
    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position =this.transform.position;
        position.y -= 0.25f;
        this.transform.position = position;
    }

    private void InvaderKilled()
    {
        amountKilled++;
        FindAnyObjectByType<StarKnightGameManager>().score += 100;
        if (amountKilled == totalInvaders)
        {
            FindAnyObjectByType<StarKnightGameManager>().WinGame();
        }
    }
}
