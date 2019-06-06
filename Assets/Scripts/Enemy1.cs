using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform playerPos;
    public Transform enemyTransform;
    public Rigidbody enemyRB;
    public Vector3 fullScale;
    public SpriteRenderer enemyRender;
    public float enemyHealth;
    public float enemyColorChange;
    public static bool active = false;
    public bool killed;

    //different enemies
    public GameObject enemy2HP;
    public GameObject enemy1HP;
    public GameObject newEnemy;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCounter.enemyCount += 1;
        enemyRB = GetComponent<Rigidbody>();
        enemyTransform = GetComponent<Transform>();
        enemyRender = GetComponent<SpriteRenderer>();
        fullScale = enemyTransform.localScale;
        enemyTransform.localScale = new Vector3(0, 0, 0);
        enemyRB.AddForce(Random.Range(100f, -100f), 0, Random.Range(100f, -100f));
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
        }

        //prevents y movement and locks rotation
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        transform.rotation = Quaternion.Euler(-90, 0, transform.rotation.z);

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

                //check tag and act accordingly
                if (gameObject.tag == "enemy1hp" || gameObject.tag == "enemyshoot")
                {
                    Destroy(gameObject);
                }

                if (gameObject.tag == "enemy2hp")
                {
                    //spawn 2 1hp enemies then die
                    newEnemy = Instantiate(enemy1HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1f), 0, Random.Range(1f, -1f));

                    newEnemy = Instantiate(enemy1HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1), 0, Random.Range(1f, -1f));

                    Destroy(gameObject);
                }

                if (gameObject.tag == "enemy3hp")
                {
                    //spawn 2 2hp enemies then die
                    newEnemy = Instantiate(enemy2HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1f), 0, Random.Range(1f, -1f));

                    newEnemy = Instantiate(enemy2HP, transform.position, transform.rotation);
                    newEnemy.transform.position += new Vector3(Random.Range(1f, -1f), 0, Random.Range(1f, -1f));

                    Destroy(gameObject);
                }
            }
        }
    }
}
