using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnHit : MonoBehaviour
{
    /* This script will disable a gameObject we can specify when the player enters a trigger component attached to this gameObject.
     * We can use this to make objects like coins disappear when the player runs into them
     * This method allows us to avoid destroying the object to allow for things like sound effects and particles to still be triggered on collision
     * Typically, we can have a child object containing this gameobject's visuals and set that as the visualsParent
     * This could also be used to make an obstacle disappear if the player enters a specific area
     */

    public GameObject objectToDisable;
    bool hasTriggered;

    private void Awake()
    {
        //if we haven't assigned anything into the visuals parent slot, we can assume we want to hide the entire object
        if (objectToDisable == null)
        {
            objectToDisable = gameObject;
        }
    }

    private void DisableObject(GameObject collidingObj)
    {
        // Check if the object that hit us has a PlatformerController script, and that we haven't already triggered
        if (collidingObj.GetComponent<PlatformerController>() && !hasTriggered) 
        {
            objectToDisable.SetActive(false);
            hasTriggered = true;

       /* Explanation
        * we are assuming that only a player character will have the platformer controller script
        * if we were to change the script the player uses, we would need to create an updated if statement
        *
        * we use the hasTriggered boolean to stop this script from executing all the time
        * Even though it probably wouldn't break, this is slightly more performant
        */

        }
    }

    #region Collision Detection
    /* Explanation
     * These two events are the ones that fire on triggers and collisions  respectivley
     * All we are doing with them is retriving the object that hit us and passing that 
     * along to our 'DisableObject' function
     */
    private void OnTriggerEnter(Collider other)
    {
        DisableObject(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        DisableObject(other.gameObject);
    }

    //2D
    private void OnTriggerEnter2D(Collider2D col)
    {
        DisableObject(col.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        DisableObject(col.gameObject);
    }
    #endregion

}
