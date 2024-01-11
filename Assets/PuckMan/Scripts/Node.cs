using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask wallLayer;
    public List<Vector2> availableDirections {  get; private set; }

    private void Start()
    {
        availableDirections = new List<Vector2> ();
        CheckAvailable(Vector2.up);
        CheckAvailable(Vector2.down);
        CheckAvailable(Vector2.left);
        CheckAvailable(Vector2.right);
    }

    private void CheckAvailable(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, this.wallLayer);

        if (hit.collider == null)
        {
            availableDirections.Add(direction);
        }
    }
}
