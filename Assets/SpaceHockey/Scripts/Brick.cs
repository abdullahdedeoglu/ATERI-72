using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {  get; private set; }
    public Sprite[] states;
    public int health { get; private set; }
    public int points = 100;

    public bool unbreakable;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!unbreakable)
        {
            health=states.Length;
            spriteRenderer.sprite = states[health-1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
    private void Hit()
    {
        if (unbreakable) return;

        health--;

        if(health <= 0)
        {
            gameObject.SetActive(false);
            FindAnyObjectByType<SpaceHockeyGameManager>().brokenBrick++;
        }
        else spriteRenderer.sprite = states[health - 1];

        FindAnyObjectByType<SpaceHockeyGameManager>().Hit(this);
    }

    

}
