using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    public Transform objectToShake; 
    public float shakeAmount = 0.1f; 
    public float shakeSpeed = 1.0f; 
    private Vector3 originalPosition;

    private void Start()
    {
        if (objectToShake == null)
            objectToShake = transform; 

        originalPosition = objectToShake.position;
    }

    private void Update()
    {
        float time = Time.time * shakeSpeed;
        float perlinX = Mathf.PerlinNoise(time, 0);
        float perlinY = Mathf.PerlinNoise(0, time);

        float offsetX = (perlinX * 2 - 1) * shakeAmount;
        float offsetY = (perlinY * 2 - 1) * shakeAmount;

        Vector3 offset = new Vector3(offsetX, offsetY, 0);

        objectToShake.position = originalPosition + offset;
    }
}
