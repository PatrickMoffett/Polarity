using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Projectile Prefab that will be spawned
    [SerializeField]
    GameObject TurretProjectilePrefab;

    //Location To Spawn The Projectile (Should be setup as a child gameobject of the Turret
    [SerializeField]
    GameObject ProjectileSpawnLocation;

    [SerializeField]
    float InitialDelay = 0;

    //How Often to launch a projectile
    [SerializeField]
    float TimeBetweenShots = 2;

    //Velocity to give the projectile after spawning
    [SerializeField]
    float ProjectileVelocity = 150;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LaunchProjectile",InitialDelay,TimeBetweenShots);
    }
    void LaunchProjectile()
    {
        //Spawn projectile at SpawnLocation with rotation, and set velocity
        GameObject Projectile = Instantiate(TurretProjectilePrefab, ProjectileSpawnLocation.transform.position, ProjectileSpawnLocation.transform.rotation);
        Projectile.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * ProjectileVelocity);
    }
}
