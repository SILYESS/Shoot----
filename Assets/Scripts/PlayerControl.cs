using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] private float runSpeed = 7.0f;
    [SerializeField] private float turnSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //Get inputs
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        //Movement action

        transform.Translate(Vector3.forward * vertInput * runSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horInput * runSpeed * Time.deltaTime);
    }

    void ConstrainMove()
    {

    }
}
