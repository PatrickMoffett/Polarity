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
        Vector2 SpawnLocation = ProjectileSpawnLocation.transform.position;

        GameObject Projectile = Instantiate(TurretProjectilePrefab,SpawnLocation, Quaternion.identity);
        Projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileVelocity, 0));
    }
}
