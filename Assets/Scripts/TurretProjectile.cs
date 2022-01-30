using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if we hit the player, tell the player to die and destroy the projectile
            collision.gameObject.GetComponent<PlayerScript>().Die();
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == gameObject.layer)
        {
            //if we hit something on our layer, kill the projectile
            Destroy(gameObject);
        }
    }
}
