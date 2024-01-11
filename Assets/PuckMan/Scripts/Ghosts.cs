using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public Movement movement {  get; private set; }
    public int point = 200;
    public Transform target;

    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostBehavior behavior;

    private void Awake()
    {
        movement=GetComponent<Movement>();
        home=GetComponent<GhostHome>();
        scatter=GetComponent<GhostScatter>();
        chase=GetComponent<GhostChase>();
        frightened=GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        
        if(this.home != this.behavior) this.home.Disable();

        if(this.behavior != null) this.behavior.Enable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Puckman"))
        {
            if (this.frightened.enabled) FindObjectOfType<PuckManGameManager>().GhostEaten(this);
            else FindObjectOfType<PuckManGameManager>().PuckManEaten();
        }
    }
}
