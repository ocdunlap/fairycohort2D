using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnHit : MonoBehaviour
{
    public AudioSource coinSource;     //Which AudioSource are we using, set this in the inspector on the script

    void TryPlaySound(GameObject collidingObj)
    {
        if (collidingObj.tag == "Player") //is the object we collided with is a player
        {
            coinSource.Play();         //play the AudioSource 
            Debug.Log("Played sound");
        }
    }

    #region Collision Detection
    void OnTriggerEnter(Collider other)
    {
        TryPlaySound(other.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        TryPlaySound(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        TryPlaySound(col.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        TryPlaySound(other.gameObject);
    }
    #endregion

}
