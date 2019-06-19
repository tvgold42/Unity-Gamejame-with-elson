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
    public string fireMode = "orange";
    public float deathTimer = 2;
    public Animator playerAnimator;
    public bool hurt = false;
    public bool death = false;
    public int playerHealth;
    public GameObject orangeBullet;
    public GameObject purpleBullet;
    public GameObject newBullet;
    private Scrollbar purpleBulletsLeftSlider;
    private Scrollbar orangeBulletsLeftSlider;
    private Scrollbar sliderMiddleImage;
    public float orangeBulletsLeft;
    public float purpleBulletsLeft;
    public GameObject AimingTarget;
    private float maxBullets;
    public GameObject GunHolder;
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Gun3;
    public AudioSource playerSound;
    public AudioClip shootSound;
    public AudioClip dieSound;
    private int screenSpaceHalfwayX;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRender = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerSound = GetComponent<AudioSource>();
        playerHealth = 3;
        purpleBulletsLeftSlider = GameObject.Find("purpleBuletsRemaining").GetComponent<Scrollbar>();
        orangeBulletsLeftSlider = GameObject.Find("orangeBuletsRemaining").GetComponent<Scrollbar>();
        sliderMiddleImage       = GameObject.Find("MiddleBulletsRemaining").GetComponent<Scrollbar>();
        maxBullets = orangeBulletsLeft + purpleBulletsLeft;
        purpleBulletsLeftSlider.size = (purpleBulletsLeft / maxBullets);
        orangeBulletsLeftSlider.size = (orangeBulletsLeft / maxBullets);
        sliderMiddleImage.value = (purpleBulletsLeft/maxBullets);
        playerAnimator = this.GetComponent<Animator>();
        screenSpaceHalfwayX = Screen.width / 2;
    }

    // Update is called once per frame
    void Update()
    {

       //input for moving/firing
       forwardInput = Input.GetAxis("Vertical");
       turnInput = Input.GetAxis("Horizontal");
        //lock rotation
        transform.rotation = Quaternion.Euler(-90, 0, 0);



        //shoot orange bullet
        if ((Input.GetKey("z") || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1)) && fireCooldown <= 0 && death == false && orangeBulletsLeft > 0)
        { //spawn and move bullet;

            //play fire sound
            playerSound.PlayOneShot(shootSound, 1f);

        fireCooldown = maxCooldown;
        newBullet = Instantiate(orangeBullet, Gun1.transform.position, Gun1.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2500);
        newBullet = Instantiate(orangeBullet, Gun2.transform.position, Gun2.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2500);
        newBullet = Instantiate(orangeBullet, Gun3.transform.position, Gun3.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2500);

          orangeBulletsLeft--;
          purpleBulletsLeft++;
          purpleBulletsLeftSlider.size = (purpleBulletsLeft / maxBullets);
          orangeBulletsLeftSlider.size = (orangeBulletsLeft / maxBullets);
          sliderMiddleImage.value = (purpleBulletsLeft/maxBullets);

          
          ////do the same on the options
          //newBullet = Instantiate(orangeBullet, option1.transform.position, option1.transform.rotation);
          //newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);
          //newBullet = Instantiate(orangeBullet, option2.transform.position, option2.transform.rotation);
          //newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * 2000);


        }

        //shoot purple bullet
        if ((Input.GetKey("z") || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && fireCooldown <= 0 && death == false  && purpleBulletsLeft > 0)
        {
            //play fire sound
            playerSound.PlayOneShot(shootSound, 1f);
            
            //spawn and move bullet;
            fireCooldown = maxCooldown;
        newBullet = Instantiate(purpleBullet, Gun1.transform.position, Gun1.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2500);
        newBullet = Instantiate(purpleBullet, Gun2.transform.position, Gun2.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2500);
        newBullet = Instantiate(purpleBullet, Gun3.transform.position, Gun3.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2500);
        purpleBulletsLeft--;
        orangeBulletsLeft++;
        purpleBulletsLeftSlider.size = purpleBulletsLeft / maxBullets;
        orangeBulletsLeftSlider.size = (orangeBulletsLeft / maxBullets);
        sliderMiddleImage.value = (purpleBulletsLeft/maxBullets);
        }



        //firing cooldown
        if (fireCooldown >= 0)
        {fireCooldown -= Time.deltaTime;}
        invulnTimer -= Time.deltaTime;

        if (hurt == true && invulnTimer <= 0 && death == false)
        { hurt = false;
          playerRender.material.color = new Color(1f, 1f, 1f, 1f);
        }

        
       
    }

    public void switchBulletType()
    {
        if (fireMode == "orange")
        {
            fireMode = "purple";
            playerAnim.SetBool("orange", false);
        }
        else if (fireMode == "purple")
        {
            fireMode = "orange";
            playerAnim.SetBool("orange", true);
        }
    }

    void FixedUpdate()
    {
        //movement
        if (death == false)
        {
            playerRB.AddForce(new Vector3(turnInput * -1, 0, forwardInput * -1) * (acceleration));

        }
        if (death == true)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0)
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }

        }
    }

    private void LateUpdate()
    {
        Vector2 mousePos = new Vector2();

        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        

        AimingTarget.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        //Debug.Log("mouse pos = " + mousePos);

        GunHolder.transform.LookAt(AimingTarget.transform);
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
        { playerRB.velocity *= -1f;
          transform.rotation = Quaternion.Euler(-90, 0, transform.rotation.z + 180);
        }

        //lose health/die when colliding with harmful things
        
        else if (other.gameObject.tag != "bound" && hurt == false && death == false)
        {
            //play hurt sound
            playerSound.PlayOneShot(dieSound, 1f);

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
                    playerAnimator.SetBool("isDead", true);

                    //for now because there is no death anim, just make character invisible
                    //playerRender.material.color = new Color(1f, 1f, 1f, 0f);
                    Gun1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                    Gun2.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                    Gun3.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);

                //option1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                //option2.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                Debug.Log("lose!!!");

            }
        }
    }
}
