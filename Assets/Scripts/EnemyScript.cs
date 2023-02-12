using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Reference to the player character
    public GameObject player;
    public GameObject enemyWeap;

    [SerializeField] private float turnSpeed;

    // Attack range of the enemy
    public float attackRange = 5.0f;

    // Damage dealt to the player when the enemy attacks
    public int attackDamage = 20;

    // Time between attacks in seconds
    public float attackRate = 1.0f;

    // Timer to track the time between attacks
    private float attackTimer = 0.0f;

    void Start()
    {

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

                // Deal damage to the player
                enemyWeap.GetComponent<EnemyWeapen>().Shoot();
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
}

