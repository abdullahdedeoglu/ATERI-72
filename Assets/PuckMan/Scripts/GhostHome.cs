using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform insideTransform;
    public Transform outsideTransform;

    private void OnDisable()
    {
        if(this.gameObject.activeSelf) StartCoroutine(ExitTransition());
    }
    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }
    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rb.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed<duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, insideTransform.position, elapsed/duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = 0.0f;
        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, outsideTransform.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        this.ghost.movement.SetDirection(new Vector2(Random.value<0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghost.movement.rb.isKinematic = false;
        this.ghost.movement.enabled = true;
    }
}
