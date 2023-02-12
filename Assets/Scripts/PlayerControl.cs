using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] private float runSpeed = 750;
    [SerializeField] private float turnSpeed = 20.0f;
    [SerializeField] private float zBound = 22.0f;
    [SerializeField] private float xBound = 15.0f;
    [SerializeField] private int amount = 20;

    private Vector3 moveDire;
    private bool isDead;
    public int currentHealth;
    public GameObject dead;
    public Joystick joystick;

    public Button restart;
    public Button exit;

    public TextMeshProUGUI hp;
    public TextMeshProUGUI gameOver;

    private AudioSource playerAudio;
    public AudioClip hit;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
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
        hp.text = currentHealth.ToString() + " %";
    }
    void MovePlayer()
    {
        //Get inputs
        float vertInput = joystick.Vertical;
        float horInput = joystick.Horizontal;

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
    private void OnTriggerEnter(Collider other)
    {
        currentHealth -= amount;
        playerAudio.PlayOneShot(hit, 1);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            isDead = true;
            Instantiate(dead, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
            WaitForSeconds.Equals(0+Time.time, 2);
            gameOver.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
