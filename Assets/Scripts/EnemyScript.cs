using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Reference to the player character
    public GameObject player;
    public GameObject dead;

    public Vector3 attackPoint;
    public ParticleSystem gunEffect;

    public GameObject bulletPrefab;
    // Speed of the bullet
    public float bulletSpeed = 20.0f;

    // Rate of fire in shots per second
    public float fireRate = 1.0f;

    // Timer to track the time between shots
    private float shotTimer = 0.0f;

    [SerializeField] private float turnSpeed;

    // Attack range of the enemy
    public float attackRange = 5.0f;

    // Damage dealt to the player when the enemy attacks
    public int attackDamage = 20;

    // Time between attacks in seconds
    public float attackRate = 1.0f;

    // Timer to track the time between attacks
    private float attackTimer = 0.0f;

    //Sound effect
    private AudioSource playerAudio;
    public AudioClip gunSound;
    public AudioClip hit;
    void Start()
    {
        gunEffect.Stop();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTo();
        // Update the attack timer
        attackTimer += Time.deltaTime;
        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            // Check if the enemy is ready to attack
            if (attackTimer >= attackRate)
            {
                // Reset the attack timer
                attackTimer = 0.0f;
                if (player.GetComponent<PlayerControl>().currentHealth > 0)
                {
                    // Deal damage to the player
                    Shoot();
                }
            }
        }
    }
    void RotateTo()
    {
        Vector3 enemyDirection = (player.transform.position - transform.position).normalized;
        if (enemyDirection.magnitude == 0) { return; }
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            var rotation = Quaternion.LookRotation(enemyDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed);
        }
    }
    void Shoot()
    {
        var gunEffectPoint = new Vector3(transform.position.x, 1f, transform.position.z);
        attackPoint = new Vector3(transform.position.x, 1.5f, transform.position.z);
        GameObject bullet = Instantiate(bulletPrefab,attackPoint , transform.rotation);
        Instantiate(gunEffect, gunEffectPoint,transform.rotation);
        gunEffect.Play();
        playerAudio.PlayOneShot(gunSound, 1);
        shotTimer = Time.time + fireRate;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(dead, new Vector3( transform.position.x,0,transform.position.z), transform.rotation);
        playerAudio.PlayOneShot(hit, 1);
    }
}

