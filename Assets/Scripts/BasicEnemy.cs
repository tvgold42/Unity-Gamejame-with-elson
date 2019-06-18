using System.Collections;
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
    public string enemyType;
    public float enemyHealth;
    public float enemyColorChange;
    public static bool active = false;
    public bool killed;

    //different enemies
    public GameObject enemy2HP;
    public GameObject enemy1HP;
    public GameObject newEnemy;
    //enemy bullet for shooter enemies
    public GameObject enemyBullet;
    public GameObject newEnemyBullet;
    public Transform bulletTarget;
    public float shootCooldown = 5f;
    

    void Start()
    {
        EnemyCounter.enemyCount += 1;
        enemyRB = GetComponent<Rigidbody>();
        enemyTransform = GetComponent<Transform>();
        enemyRender = GetComponent<SpriteRenderer>();
        enemySound = GetComponent<AudioSource>();
        fullScale = enemyTransform.localScale;
        enemyTransform.localScale = new Vector3(0, 0, 0);
        enemyRB.AddForce(Random.Range(100f, -100f), 0, Random.Range(100f, -100f));
        //target for bullet
        bulletTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //flash color when hit
        enemyColorChange -= Time.deltaTime;
        if (enemyColorChange <= 0)
        { enemyRender.material.color = Color.white; }

        //grow to full size after spawning
        if (enemyTransform.localScale.x < fullScale.x)
        { enemyTransform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
          enemyRB.detectCollisions = false; }

        if (enemyTransform.localScale.x >= fullScale.x)
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

      // // limiting max speed
        //if (enemyRB.velocity.magnitude >= 4f && active == true)
        //{ enemyRB.velocity = enemyRB.velocity.normalized; }

        //countdown to shoot
        shootCooldown -= Time.deltaTime;
        //shooting
        if (shootCooldown <= 0 && enemyType == "enemyshoot")
        { newEnemyBullet = Instantiate(enemyBullet, transform.position, transform.rotation);
          Debug.Log("Bullet Fired at " + transform.position);
          newEnemyBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector2.up * -800);
          shootCooldown = 5f; }



    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "whiteBullet" && enemyType == "enemyblack")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "blackBullet" && enemyType == "enemywhite")
        {
            Destroy(other.gameObject);
        }

        else if ((other.gameObject.tag == "whiteBullet" || other.gameObject.tag == "blackBullet") && killed == false)
        {
            //play hurt sound
            enemySound.PlayOneShot(enemyHurt, 1f);

            enemyHealth -= 1;
            enemyRender.material.color = Color.cyan;
            enemyColorChange = 0.07f;

            if (enemyHealth <= 0)
            {
                //play hurt sound
                enemySound.PlayOneShot(enemyDie, 1f);
                GameObject.Find("EnemySpawnHandler").GetComponent<Enemy_Counter_Designers_Test>().totalKilledSoFar++;
                killed = true;
                Debug.Log("killed enemy");
                EnemyCounter.enemyCount -= 1;
                Destroy(other.gameObject);

                //check enemy type and act accordingly

                if (enemyType == "enemy2hp" || enemyType == "enemyshoot")
                {
                    //spawn 2 1hp enemies then die
                    newEnemy = Instantiate(enemy1HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1f), 0, Random.Range(1f, -1f));

                    newEnemy = Instantiate(enemy1HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1), 0, Random.Range(1f, -1f));

                    Destroy(gameObject);
                    Score.score += 300;
                }

                if (enemyType == "enemy3hp")
                {
                    //spawn 2 2hp enemies then die
                    newEnemy = Instantiate(enemy2HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1f), 0, Random.Range(1f, -1f));

                    newEnemy = Instantiate(enemy2HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1f), 0, Random.Range(1f, -1f));

                    Destroy(gameObject);
                    Score.score += 500;
                }

                else
                {
                    Destroy(gameObject);
                    Score.score += 200;
                    
                }
            }
        }
    }
}
