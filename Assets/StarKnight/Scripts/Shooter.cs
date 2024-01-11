using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooter : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 1.0f;

    private bool laserActive;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        float direction = Input.GetAxis("Horizontal");
        //Debug.Log(direction);
        if (direction <= -0.5f)
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        else if (direction >= 0.5f)
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!laserActive)
        {
            Projectile projectile = Instantiate(laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            laserActive = true;
        }
        
    }

    private void LaserDestroyed()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Invader") || other.gameObject.layer==LayerMask.NameToLayer("Missile"))
        {
            FindAnyObjectByType<StarKnightGameManager>().LooseGame();
        }
    }
}
