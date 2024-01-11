using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrominos : MonoBehaviour
{
    float fall = 0;
    private float fallSpeed = 1;
    public bool allowRotation = true;
    public bool limitRotation = false;
    
    public int invidualScore = 100;
    private float invidualScoreTime;

    public AudioClip moveSound;
    public AudioClip rotateSound;
    public AudioClip landSound;

    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fallSpeed = GameObject.Find("Grid").GetComponent<Games>().fallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Games.isPaused)
        {
            CheckUserInput();
            UpdateInvidualScore();
        }
        
    }

    void UpdateInvidualScore()
    {
        if (invidualScoreTime <1)
        {
            invidualScoreTime += Time.deltaTime;
        }
        else
        {
            invidualScoreTime = 0;
            invidualScore = Mathf.Max(invidualScore - 10, 0);
        }
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (CheckIsValidPosition())
            {
                FindAnyObjectByType<Games>().UpdateGrid(this);
                PlayMoveAudio();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (CheckIsValidPosition())
            {
                FindAnyObjectByType<Games>().UpdateGrid(this);
                PlayMoveAudio();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(allowRotation)
            {
                if(limitRotation)
                {
                    if(transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }

                if (CheckIsValidPosition())
                {
                    FindAnyObjectByType<Games>().UpdateGrid(this);
                    PlayRotateAudio();
                }
                else
                {
                    if (limitRotation)
                    {
                        if(transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else { transform.Rotate(0, 0, -90); }
                }
                
            }
            
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            transform.position += new Vector3(0, -1, 0);
            //Debug.Log("Position is: "+transform.position.y);
            
            if (CheckIsValidPosition())
            {
                FindAnyObjectByType<Games>().UpdateGrid(this); 
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //Debug.Log("down key pressed?");
                    PlayMoveAudio();
                }
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                FindAnyObjectByType<Games>().DeleteRow();
                if (FindAnyObjectByType<Games>().CheckIsAboveGrid(this))
                {
                    FindAnyObjectByType<Games>().GameOver();
                }
                
                PlayLandAudio();
                FindAnyObjectByType<Games>().SpawnNextTetromino();
                Games.currentScore += invidualScore;
                enabled = false;
                tag = "Untagged";
            }

            fall = Time.time;
        }
    }

    void PlayMoveAudio()
    {
        audioSource.PlayOneShot(moveSound);
    }

    void PlayRotateAudio()
    {
        audioSource.PlayOneShot(rotateSound);
    }

    void PlayLandAudio()
    {
        audioSource.PlayOneShot(landSound);
    }
    private bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector3 pos = FindAnyObjectByType<Games>().Round(mino.position);
            //Debug.Log(pos);
            if (FindAnyObjectByType<Games>().CheckIsInsýdeGrid(pos)== false)
            {
                //Debug.Log("option a");
                return false;
            }
            if (FindAnyObjectByType<Games>().GetTransformGridPosition(pos) != null && FindAnyObjectByType<Games>().GetTransformGridPosition(pos).parent != transform)
            {
                //Debug.Log("AA: " + FindAnyObjectByType<Games>().GetTransformGridPosition(pos));
                //Debug.Log("BB: " + FindAnyObjectByType<Games>().GetTransformGridPosition(pos).parent);
                //Debug.Log("CC: " + transform);
                return false;               
            }
        }
        //Debug.Log("Hella far");
        return true;

    }

}
