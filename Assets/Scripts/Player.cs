using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Rigidbody playerRB;
    public SpriteRenderer playerRender;
    public Animator playerAnim;
    public float forwardInput;
    public float turnInput;
    public float acceleration;
    public float fireCooldown;
    public float maxCooldown;
    public float invulnTimer;
    public string fireMode = "white";
    public float deathTimer = 2;
    public Animator playerAnimator;

    public bool hurt = false;
    public bool death = false;
    public int playerHealth;
    public GameObject whiteBullet;
    public GameObject blackBullet;
    public GameObject newBullet;


    //Option 1 and 2 are currently just used 
    public GameObject option1;
    public GameObject option2;

    private Scrollbar whiteBulletsLeftSlider;
    public float whiteBulletsLeft = 30;
    public GameObject AimingTarget;
    public int maxBullets;
    public GameObject Gun;

    private int screenSpaceHalfwayX;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRender = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerHealth = 3;
        whiteBulletsLeftSlider = GameObject.Find("bullet type remaining").GetComponent<Scrollbar>();
        whiteBulletsLeftSlider.size = (whiteBulletsLeft / maxBullets);
        playerAnimator = this.GetComponent<Animator>();
        screenSpaceHalfwayX = Screen.width / 2;
    }

    // Update is called once per frame
    void Update()
    {

       //input for moving/firing
       forwardInput = Input.GetAxis("Vertical");
       turnInput = Input.GetAxis("Horizontal");



        //shoot white bullet
        if ((Input.GetKey("z") || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && fireCooldown <= 0 && death == false && fireMode == "white" && whiteBulletsLeft > 0)
        { //spawn and move bullet;


            fireCooldown = maxCooldown;
          newBullet = Instantiate(whiteBullet, Gun.transform.position, Gun.transform.rotation);
          newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2000);
          whiteBulletsLeft--;
          whiteBulletsLeftSlider.size = (whiteBulletsLeft / maxBullets);
          
          ////do the same on the options
          //newBullet = Instantiate(whiteBullet, option1.transform.position, option1.transform.rotation);
          //newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
          //newBullet = Instantiate(whiteBullet, option2.transform.position, option2.transform.rotation);
          //newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);


        }

        //shoot black bullet
        if ((Input.GetKey("z") || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && fireCooldown <= 0 && death == false && fireMode == "black" && whiteBulletsLeft < maxBullets)
        { //spawn and move bullet;
          fireCooldown = maxCooldown;
            newBullet = Instantiate(blackBullet, Gun.transform.position, Gun.transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2000);

          whiteBulletsLeft++;
          whiteBulletsLeftSlider.size = whiteBulletsLeft / maxBullets;

            ////do the same on the options
            //newBullet = Instantiate(blackBullet, option1.transform.position, option1.transform.rotation);
            //newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
            //newBullet = Instantiate(blackBullet, option2.transform.position, option2.transform.rotation);
            //newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
        }

        //change firing mode between white and black
        if ((Input.GetKeyDown("e") || Input.GetKeyDown("x") || Input.GetMouseButtonDown(1)) && death == false)
        {   if (fireMode == "white")
            {fireMode = "black";
             playerAnim.SetBool("White", false);}
            else if (fireMode == "black")
            {fireMode = "white";
             playerAnim.SetBool("White", true);}
        }


        //firing cooldown
        if (fireCooldown >= 0)
        {fireCooldown -= Time.deltaTime;}
        invulnTimer -= Time.deltaTime;

        if (hurt == true && invulnTimer <= 0 && death == false)
        { hurt = false;
          playerRender.material.color = new Color(1f, 1f, 1f, 1f);
          //option1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
          //option2.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
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
            //  playerRB.AddRelativeForce(Vector2.up * forwardInput * (acceleration));
            //playerRB.AddTorque(0, turnInput * 3f, 0);
            playerRB.AddForce(new Vector3(turnInput * -1, 0, forwardInput * -1) * (acceleration));

        }
        if (death == true)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0)
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }
            // playerRB.AddTorque(0, turnInput * 3f, 0);

        }
    }

    private void LateUpdate()
    {
        Vector2 mousePos = new Vector2();

        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        

        AimingTarget.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        //Debug.Log("mouse pos = " + mousePos);

        Gun.transform.LookAt(AimingTarget.transform);
        if (mousePos.x > screenSpaceHalfwayX)
        {
            playerAnimator.SetBool("isLookingLeft", true);
        }
        else
        {
            playerAnimator.SetBool("isLookingLeft", false);
        }
    }


    void OnCollisionEnter(Collision other)
    {
        //for boucning off the edges of the stage
        //will work once we add a 3d ring
        if (other.gameObject.tag == "bound")
        { playerRB.velocity *= 1.5f;
          transform.rotation = Quaternion.Euler(-90, 0, transform.rotation.z + 180);
        }

        //lose health/die when colliding with harmful things
        
        else if (other.gameObject.tag != "bound" && hurt == false && death == false)
        {

                hurt = true;
                invulnTimer = 2;
                playerHealth -= 1;
                playerRender.material.color = new Color(1f, 1f, 1f, 0.5f);
                //option1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                //option2.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                playerRB.velocity *= -3;
                Debug.Log("damage!!!");
            //check if health 0;
            if (playerHealth <= 0)
                {
                    death = true;
                    //play a player splat animation
                    //set a timer for transitioning to the game over screen

                    //for now because there is no death anim, just make character invisible
                    playerRender.material.color = new Color(1f, 1f, 1f, 0f);
                    Gun.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);

                //option1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                //option2.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                Debug.Log("lose!!!");

            }
        }
    }
}
