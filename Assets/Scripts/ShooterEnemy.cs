using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public Transform playerPos;
    public Transform enemyTransform;
    private Transform target;
    public Rigidbody enemyRB;
    public Vector3 fullScale;
    public SpriteRenderer enemyRender;
    public float shootCooldown = 5f;
    public float enemyHealth;
    public float enemyColorChange;
    public bool active = false;
    public bool killed;

    //different enemies
    public GameObject enemy1HP;
    public GameObject newEnemy;
    public GameObject enemyBullet;
    public GameObject newEnemyBullet;

    

    void Start()
    {
        EnemyCounter.enemyCount += 1;
        enemyRB = GetComponent<Rigidbody>();
        enemyTransform = GetComponent<Transform>();
        enemyRender = GetComponent<SpriteRenderer>();
        fullScale = enemyTransform.localScale;
        enemyTransform.localScale = new Vector3(0, 0, 0);
        enemyRB.AddForce(Random.Range(100f, -100f), 0, Random.Range(100f, -100f));
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //flash color when hit
        enemyColorChange -= Time.deltaTime;
        if (enemyColorChange <= 0)
        {
            enemyRender.material.color = Color.white;
        }

        //countdown to shoot
        shootCooldown -= Time.deltaTime;
        //shooting
        if (shootCooldown <= 0)
        { 
            
            newEnemyBullet = Instantiate(enemyBullet, transform.position, transform.rotation);
            newEnemyBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * -800);

            shootCooldown = 5f;

        }
        //grow to full size after spawning
        if (enemyTransform.localScale.x < fullScale.x)
        {
            enemyTransform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            enemyRB.detectCollisions = false;
        }

        if (enemyTransform.localScale.x >= fullScale.x)
        {
            active = true;
            enemyRB.detectCollisions = true;
        }

        if (active == true)
        {
            Vector3 relativePos = GameObject.Find("player").transform.position - gameObject.transform.position;
            enemyRB.AddForce(0.08f * relativePos);

            transform.LookAt(playerPos.position * -1f, transform.up * -50000f);
        }

        //prevents y movement and locks rotation
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);

        //limiting max speed
        if (enemyRB.velocity.magnitude >= 4f && active == true)
        {
            enemyRB.velocity = enemyRB.velocity.normalized;
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "friendlyBullet" && killed == false)
        {
            enemyHealth -= 1;
            enemyRender.material.color = Color.cyan;
            enemyColorChange = 0.07f;

            if (enemyHealth <= 0)
            {
                killed = true;
                Debug.Log("killed enemy");
                EnemyCounter.enemyCount -= 1;
                Destroy(other.gameObject);


                if (gameObject.tag == "enemyshoot")
                {
                    //spawn 2 1hp enemies then die
                    newEnemy = Instantiate(enemy1HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1f), 0, Random.Range(1f, -1f));

                    newEnemy = Instantiate(enemy1HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1), 0, Random.Range(1f, -1f));

                    Destroy(gameObject);
                }
            }
        }
    }
}
