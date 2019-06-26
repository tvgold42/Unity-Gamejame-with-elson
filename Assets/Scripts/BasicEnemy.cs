﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Transform playerPos;
    public Transform enemyTransform;
    public Rigidbody enemyRB;
    public Vector3 fullScale;
    public SpriteRenderer enemyRender;
    public AudioSource enemySound;
    public AudioClip enemyHurt;
    public AudioClip enemyDie;
    public Animator enemyAnim;
    public string enemyType;
    public float enemyHealth;
    public float enemyColorChange;
    public static bool active = false;
    public bool killed;
    public float deathTime = 0;

    //different enemies
    public GameObject enemy2HP;
    public GameObject enemy1HP;
    public GameObject nextEnemyToSpawn;
    public GameObject newEnemy;
    //enemy bullet for shooter enemies
    public GameObject enemyBullet;
    public GameObject newEnemyBullet;
    public Transform bulletTarget;
    public float shootCooldown = 5f;

    //dashing towards player timer
    public float dashCooldown = 10;
    

    void Start()
    {
        EnemyCounter.enemyCount += 1;
        enemyRB = GetComponent<Rigidbody>();
        enemyTransform = GetComponent<Transform>();
        enemyRender = GetComponent<SpriteRenderer>();
        enemySound = GetComponent<AudioSource>();
        enemyAnim = GetComponent<Animator>();
        fullScale = enemyTransform.localScale;
        enemyTransform.localScale = new Vector3(0, 0, 0);
        enemyRB.AddForce(Random.Range(100f, -100f), 0, Random.Range(100f, -100f));
        //target for bullet
        bulletTarget = GameObject.FindGameObjectWithTag("Player").transform;

        //move left/right slightly so that enemies created via splitting dont get stuck in each other
        transform.position = new Vector3(transform.position.x + Random.Range(-4f, 4f), transform.position.y, transform.position.z + Random.Range(-4f, 4f));
    }

    void Update()
    {
        if (killed == true)
        {
            enemyRB.detectCollisions = false;
            deathTime += Time.deltaTime;
        }
        if (deathTime >= 0.4f)
        {
            Destroy(gameObject);
        }
        //stop moving if player is dead
        if(Player.death == true)
        {
            enemyRB.velocity = Vector3.zero;
        }
        //flash color when hit
        enemyColorChange -= Time.deltaTime;
        if (enemyColorChange <= 0)
        { enemyRender.material.color = Color.white; }

        //flip sprite depending on where player is
        /*
        if (transform.position.x >= playerPos.position.x && transform.localScale.x >= 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (transform.position.x <= playerPos.position.x && transform.localScale.x <= 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        */
        //grow to full size after spawning
        if (enemyTransform.localScale.y < fullScale.y)
        { enemyTransform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
          enemyRB.detectCollisions = false; }

        if (enemyTransform.localScale.y >= fullScale.y)
        { active = true;
          enemyRB.detectCollisions = true;}

        //move towards player
        if (active == true && enemyType != "enemyshoot")
        { Vector3 relativePos = GameObject.Find("player").transform.position - gameObject.transform.position;
          enemyRB.AddForce(1f * relativePos);
          transform.rotation = Quaternion.Euler(-90, 0, transform.rotation.z); }

        //lock y position
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //make shooter enemies aim towards player
        if (enemyType == "enemyshoot")
        { transform.LookAt(playerPos.position * -1f, transform.up * -50000f);
          transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);}

        // limiting max speed
        if (enemyRB.velocity.magnitude >= 20f && active == true)
        { enemyRB.velocity = enemyRB.velocity.normalized; }

        //countdown to shoot/dash
        shootCooldown -= Time.deltaTime;
        dashCooldown -= Time.deltaTime;
        //shooting
        if (shootCooldown <= 0 && enemyType == "enemyshoot")
        { newEnemyBullet = Instantiate(enemyBullet, transform.position, transform.rotation);
          Debug.Log("Bullet Fired at " + transform.position);
          newEnemyBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * -800);
          shootCooldown = 5f; }
        //dashing
        if (dashCooldown <= 0 && (enemyType == "enemy1hp" || enemyType == "enemy2hp" || enemyType == "enemy3hp"))
        {
            enemyRB.velocity = Vector3.zero;
            Vector3 relativePos = GameObject.Find("player").transform.position - gameObject.transform.position;
            enemyRB.AddForce(1f * relativePos * 300);
            dashCooldown = 10f;
        }



    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "purpleBullet" && killed == false)
        {
            //play hurt sound
            enemySound.PlayOneShot(enemyHurt, 1f);
            enemyAnim.SetBool("HasTakenDamage", true);
            enemyHealth -= 1;
            enemyRender.material.color = Color.cyan;
            enemyColorChange = 0.07f;

            if (enemyHealth <= 0)
            {
                //play hurt sound
                enemySound.PlayOneShot(enemyDie, 1f);
                GameObject.Find("EnemySpawnHandler").GetComponent<Enemy_Counter_Designers_Test>().badGuyDied();
                killed = true;
                Debug.Log("killed enemy");
                EnemyCounter.enemyCount -= 1;
                enemyAnim.SetBool("IsDead", true);
                Destroy(other.gameObject);
                enemyRB.velocity = Vector3.zero;


                //check enemy type and act accordingly

                if (enemyType != "enemy1hp" )
                {
                    //spawn 2 of the next bad guys
                    //  GameObject.Find("EnemySpawnHandler").GetComponent<Enemy_Counter_Designers_Test>().spawnNewBadguy(nextEnemyToSpawn);
                    Instantiate(nextEnemyToSpawn, transform.position, transform.rotation);
                    Instantiate(nextEnemyToSpawn, transform.position, transform.rotation);
                   // Destroy(gameObject);
                    Score.score += 300; 

                }

                else
                {
                    Score.score += 200;
                    //Destroy(gameObject);
   
                }
            }
        }
    }
}
