using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int point = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Puckman"))
        {
            Eat();
        }
    }

    protected virtual void Eat()
    {
        FindObjectOfType<PuckManGameManager>().PellettEaten(this);
    }
}
