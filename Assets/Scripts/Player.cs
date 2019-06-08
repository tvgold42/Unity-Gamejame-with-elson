using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRB;
    public SpriteRenderer playerRender;
    public Animator playerAnim;
    public float forwardInput;
    public float turnInput;
    public float acceleration;
    public float fireCooldown;
    public float invulnTimer;
    public string fireMode = "white";

    public bool hurt = false;
    public bool death = false;
    public int playerHealth;
    public GameObject whiteBullet;
    public GameObject blackBullet;
    public GameObject newBullet;


    //Option 1 and 2 are currently just used 
    public GameObject option1;
    public GameObject option2;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRender = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerHealth = 3;
        

    }

    // Update is called once per frame
    void Update()
    {

       //input for moving/firing
       forwardInput = Input.GetAxis("Vertical");
       turnInput = Input.GetAxis("Horizontal");
       //shoot white bullet
       if ((Input.GetKey("z") || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && fireCooldown <= 0 && death == false && fireMode == "white")
        { //spawn and move bullet;
          fireCooldown = 0.1f;
          newBullet = Instantiate(whiteBullet, transform.position, transform.rotation);
          newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
          //do the same on the options
          newBullet = Instantiate(whiteBullet, option1.transform.position, option1.transform.rotation);
          newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
          newBullet = Instantiate(whiteBullet, option2.transform.position, option2.transform.rotation);
          newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
        }

        //shoot bullet
        if ((Input.GetKey("z") || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && fireCooldown <= 0 && death == false && fireMode == "black")
        { //spawn and move bullet;
          fireCooldown = 0.1f;
          newBullet = Instantiate(blackBullet, transform.position, transform.rotation);
          newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
          //do the same on the options
          newBullet = Instantiate(blackBullet, option1.transform.position, option1.transform.rotation);
          newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
          newBullet = Instantiate(blackBullet, option2.transform.position, option2.transform.rotation);
          newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
        }

        //change firing mode between white and black
        if ((Input.GetKeyDown("e") || Input.GetKeyDown("x")) && death == false)
        {
            switchBulletType();
        }


        //firing cooldown
        if (fireCooldown >= 0)
        {fireCooldown -= Time.deltaTime;}
        invulnTimer -= Time.deltaTime;

        if (hurt == true && invulnTimer <= 0 && death == false)
        { hurt = false;
          playerRender.material.color = new Color(1f, 1f, 1f, 1f);
          option1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
          option2.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }

        
       
    }

    public void switchBulletType()
    {
        if (fireMode == "white")
        {
            fireMode = "black";
            playerAnim.SetBool("White", false);
        }
        else if (fireMode == "black")
        {
            fireMode = "white";
            playerAnim.SetBool("White", true);
        }
    }

    void FixedUpdate()
    {
        //movement
        if (death == false)
        {
            playerRB.AddRelativeForce(Vector2.up * forwardInput * (acceleration));
            playerRB.AddTorque(0, turnInput * 4, 0);
        }
    }


    void OnCollisionEnter(Collision other)
    {
        //for boucning off the edges of the stage
        //will work once we add a 3d ring
        if (other.gameObject.tag == "bound")
        { playerRB.velocity *= -1; }

        //lose health/die when colliding with harmful things
        else if (other.gameObject.tag == "enemy" && hurt == false && death == false)
        {
            hurt = true;
            invulnTimer = 2;
            playerHealth -= 1;
            playerRender.material.color = new Color(1f, 1f, 1f, 0.5f);
            option1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
            option2.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
            playerRB.velocity *= -3;
            //check if health 0;
            if (playerHealth <= 0)
            {
                death = true;
                //play a player splat animation
                //set a timer for transitioning to the game over screen

                //for now because there is no death anim, just make character invisible
                playerRender.material.color = new Color(1f, 1f, 1f, 0f);
                option1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                option2.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                Debug.Log("lose!!!");
            }
            
        }
    }
}
