using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script enables a gameObject we specify when the player collides with the trigger component attached to this gameObject
// a good use for this could be to make a victory screen appear when we reach the end of a level

public class EnableOnHit : MonoBehaviour
{
    public GameObject ObjectToEnable;

    // Start is called before the first frame update
    void Start()
    {
        //when the game starts, set visuals to inactive if it isn't already
        ObjectToEnable.SetActive(false);
    }

    private void EnableObject(GameObject collidingObj)
    {
        if (collidingObj.tag == "Player")
        {
            ObjectToEnable.SetActive(true);
        }
    }

    #region Collision Detection
    //when entering the collision, the visualsParent is enabled
    private void OnTriggerEnter(Collider other)
    {
        EnableObject(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        EnableObject(other.gameObject);
    }

    //2d
    private void OnTriggerEnter2D(Collider2D col)
    {
        EnableObject(col.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        EnableObject(col.gameObject);
    }
    #endregion
}
