﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody playerRB;
    public float forwardInput;
    public float turnInput;
    public float acceleration;
    public float fireCooldown;

    public GameObject bullet;
    public GameObject newBullet;


    //Option 1 and 2 are currently just used 
    public GameObject option1;
    public GameObject option2;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {

        //input for moving/firing
        //note for myself, have a small firing cooldown and make the space press work every frame
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        if ((Input.GetKey("z") || Input.GetKey(KeyCode.Space)) && fireCooldown <= 0)
        {
            //spawn and move bullet;
            fireCooldown = 0.1f;
            newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
            //do the same on the options
            newBullet = Instantiate(bullet, option1.transform.position, option1.transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
            newBullet = Instantiate(bullet, option2.transform.position, option2.transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);


        }

        //firing cooldown
        if (fireCooldown >= 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        //movement
        playerRB.AddRelativeForce(Vector2.up * forwardInput * (acceleration));
        playerRB.AddTorque(0, turnInput * 4, 0);

        //locks the x and z rotation as well as y position
        //transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);
        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        
    }

    //for boucning off the edges of the stage
    //will work once we add a 3d ring
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bound")
        {
            playerRB.velocity *= -1;
        }

        else
        { 
            Debug.Log("lose!!!");
        }
    }
}
