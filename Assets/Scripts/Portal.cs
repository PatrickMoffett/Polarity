using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    GameObject ExitPortal;

    //bool used to ignore a collision when something gets teleported to it
    bool bIgnoreNextCollision = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            //Iff we are supposed to ignore the next collision, ignore it and listen for a collision after this one.
            if (bIgnoreNextCollision)
            {
                bIgnoreNextCollision = false;
                return;
            }
            else
            {
                //otherwise, teleport to the other portal and tell the other portal to ignore the collision from teleporting something to it.
                ExitPortal.GetComponent<Portal>().bIgnoreNextCollision = true;
                collision.gameObject.transform.position = ExitPortal.transform.position;
            }
        }
    }

}
