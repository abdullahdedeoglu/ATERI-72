using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkGameOver : MonoBehaviour
{
    public Image imageToBlink;
    public float blinkInterval = 0.5f;

    private void Start()
    {
        // Start the blinking coroutine
        StartCoroutine(BlinkingCoroutine());
    }

    private IEnumerator BlinkingCoroutine()
    {
        while (true)
        {
            // Toggle the visibility of the Image component
            imageToBlink.enabled = !imageToBlink.enabled;

            // Wait for the specified interval
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}




