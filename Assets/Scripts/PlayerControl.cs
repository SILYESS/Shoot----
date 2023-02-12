using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] private float runSpeed = 1000;
    [SerializeField] private float turnSpeed = 20.0f;
    [SerializeField] private float zBound = 22.0f;
    [SerializeField] private float xBound = 15.0f;

    private Vector3 moveDire;
    private bool isDead;
    [SerializeField] private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        currentHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotateTo();
        ConstrainMove();
    }
    void MovePlayer()
    {
        //Get inputs
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        //Movement action
        playerRb.velocity = new Vector3(horInput * runSpeed * Time.deltaTime,0 , vertInput * runSpeed * Time.deltaTime);
        
        

       /* transform.Translate(Vector3.forward * vertInput * runSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horInput * runSpeed * Time.deltaTime);*/
    }

    void RotateTo()
    {
        if (playerRb.velocity.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(playerRb.velocity);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed);
    }

    void ConstrainMove()
    {
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        } else if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        else if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }
}
