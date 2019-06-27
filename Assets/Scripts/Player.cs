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
    public float deathTimer = 2;
    public bool hurt = false;
    public static bool death = false;
    public static int playerHealth = 3;
    public GameObject purpleBullet;
    public GameObject newBullet;
    public GameObject AimingTarget;
    public GameObject GunHolder;
    public GameObject Gun1;
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
        death = false;
        //screenspace is used to detect whether the mouse is left or right to the character
        //so the character faces the right direction
        screenSpaceHalfwayX = Screen.width / 2;
    }

    void Update()
    {
       //input for moving/firing
       forwardInput = Input.GetAxis("Vertical");
       turnInput = Input.GetAxis("Horizontal");
       //lock rotation
       transform.rotation = Quaternion.Euler(-90, 0, 0);

        //shoot purple bullet
        if ((Input.GetKey("z") || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && fireCooldown <= 0 && death == false)
        {
        //play fire sound
        playerSound.PlayOneShot(shootSound, 1f);
            
        //spawn and move bullet;
        fireCooldown = maxCooldown;
        newBullet = Instantiate(purpleBullet, Gun1.transform.position, Gun1.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.down * 2500);
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
        //gun aim at mouse
        GunHolder.transform.LookAt(AimingTarget.transform);
        if (mousePos.x > screenSpaceHalfwayX)
        {
            playerAnim.SetBool("isLookingLeft", true);
        }
        else
        {
            playerAnim.SetBool("isLookingLeft", false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //for boucning off the edges of the stage
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
                playerRB.velocity *= -3;
                Debug.Log("damage!!!");
            //check if health 0;
            if (playerHealth <= 0)
                {
                    death = true;
                    //move camera forward
                    CameraMovement.myRB.AddForce(0, -2000, 0);
                    //play a player splat animation
                    //set a timer for transitioning to the game over screen
                    playerAnim.SetBool("isDead", true);
                    playerRender.material.color = new Color(1f, 1f, 1f, 1f);
                    playerRB.velocity = Vector3.zero;
                    Gun1.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
                    Debug.Log("lose!!!");

            }
        }
    }
}
