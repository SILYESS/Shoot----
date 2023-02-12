using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeapenScript : MonoBehaviour
{
    // Prefab for the bullet
    public GameObject bulletPrefab;

    //Fire button
    public Button fire;
    public bool isfire;

    // Speed of the bullet
    public float bulletSpeed = 20.0f;

    // Rate of fire in shots per second
    public float fireRate = 1.0f;

    // Timer to track the time between shots
    private float shotTimer = 0.0f;
    // Start is called before the first frame update
    public ParticleSystem gunEffect;

    private AudioSource playerAudio;
    public AudioClip gunSound;
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();   
    }
    public void Shoot()
    {
        fire.onClick.AddListener(delegate { isfire = true; });
        
        // Update the shot timer
        shotTimer += Time.deltaTime;

        // Check if the fire button is pressed
        if ( isfire && shotTimer >= 1.0f / fireRate)
        {
            // Reset the shot timer
            shotTimer = 0.0f;

            // Instantiate a bullet and set its position and rotation to match the weapon
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Instantiate(gunEffect, transform.position, transform.rotation);
            gunEffect.Play();
            playerAudio.PlayOneShot(gunSound, 1);

            // Get the Rigidbody component of the bullet and set its velocity
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = -transform.right * bulletSpeed;
            isfire = false;
        }
    }
}

