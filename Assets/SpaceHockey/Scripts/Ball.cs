using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    public float speed = 500f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        Invoke(nameof(SetRandomTrajectory), 5f);
    }

    public void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        rb.AddForce(force.normalized * speed);
    }

    public void SetRandomTrajectoryAfterMiss()
    {
        Invoke(nameof(SetRandomTrajectory), 1f);
    }
    void Update()
    {
        
    }
}
