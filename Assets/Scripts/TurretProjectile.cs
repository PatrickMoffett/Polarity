using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //if we hit the player, tell the player to die
            collision.gameObject.GetComponent<PlayerScript>().Die();

        }
        //no matter what we hit, kill the projectile
        Destroy(gameObject);       
    }
}
