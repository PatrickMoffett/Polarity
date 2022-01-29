using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    GameObject TurretProjectilePrefab;

    [SerializeField]
    GameObject ProjectileSpawnLocation;

    [SerializeField]
    float TimeBetweenShots = 2;

    [SerializeField]
    float ProjectileVelocity = 150;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LaunchProjectile", TimeBetweenShots, TimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LaunchProjectile()
    {
        //Spawn projectile at SpawnLocation with rotation, and set velocity
        GameObject Projectile = Instantiate(TurretProjectilePrefab, ProjectileSpawnLocation.transform.position, ProjectileSpawnLocation.transform.rotation);
        Projectile.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * ProjectileVelocity);
    }
}
