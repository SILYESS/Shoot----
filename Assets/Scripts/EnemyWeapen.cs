using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapen : MonoBehaviour
{
    // Prefab for the bullet
    public GameObject enemy;
    public GameObject bulletPrefab;

    // Speed of the bullet
    public float bulletSpeed = 20.0f;

    // Rate of fire in shots per second
    public float fireRate = 1.0f;

    // Timer to track the time between shots
    private float shotTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        // Update the shot timer
        shotTimer += Time.deltaTime;

        // Check if the fire button is pressed
        if (shotTimer >= 1.0f / fireRate)
        {
            // Reset the shot timer
            shotTimer = 0.0f;

            // Instantiate a bullet and set its position and rotation to match the weapon
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            // Get the Rigidbody component of the bullet and set its velocity
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = -transform.right * bulletSpeed;
        }
    }
}
