using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlinkingEffect : MonoBehaviour
{
    public float blinkDuration = 1.0f;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = true;
    private float duration = 1.0f;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartBlink();
    }

    //private void FixedUpdate()
    //{
    //    if (Input.anyKey)
    //    {
    //        Debug.Log("A key or mouse click has been detected");
    //        Application.LoadLevel("MainMenu");
    //    }
    //}

    private System.Collections.IEnumerator BlinkSprite()
    {
        isBlinking = false;

        while (true)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(duration);
        }

    }

    private System.Collections.IEnumerator DontBlink()
    {
        isBlinking = true;

        while (true)
        {
            spriteRenderer.enabled=true;
            yield return new WaitForSeconds(duration);
        }

    }
    void StartBlink()
    {
        if (isBlinking)
        {
            StartCoroutine(BlinkSprite());
        }
        else
        {
            StartCoroutine(DontBlink());
        }
    }


}