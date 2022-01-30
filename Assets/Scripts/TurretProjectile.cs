using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if we hit the player, tell the player to die and destroy the projectile
            collision.gameObject.GetComponent<PlayerScript>().Die();
            m_AudioSource.Play();
            //Destroy(gameObject);
        }
        else if (collision.gameObject.layer == gameObject.layer)
        {
            //if we hit something on our layer, kill the projectile
            Destroy(gameObject);
        }
    }
}
