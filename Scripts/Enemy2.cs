using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Transform playerPos;
    public Rigidbody enemyRB;
    private Transform targetPosition;
    public Transform enemyTransform;
    public Vector3 fullScale;
    public float shootCooldown = 5f;

    public bool active = false;
    public bool killed;

    //different enemies
    public GameObject enemy1HP;
    public GameObject newEnemy;
    public GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        enemyTransform = GetComponent<Transform>();

        fullScale = enemyTransform.localScale;

        enemyTransform.localScale = new Vector3(0, 0, 0);
        EnemyCounter.enemyCount += 1;

        enemyRB.AddForce(Random.Range(100f, -100f), 0, Random.Range(100f, -100f));
    }

    // Update is called once per frame
    void Update()
    {
        //countdown to shoot
        shootCooldown -= Time.deltaTime;
        //shooting
        if (shootCooldown <= 0)
        {
            Vector3 relativePos = GameObject.Find("player").transform.position - gameObject.transform.position;
            
            Instantiate(enemyBullet, transform.position, transform.rotation);
          //  enemyBullet.transform.position = Vector3.MoveTowards(transform.position, playerPos.transform.position, 5f);
            enemyBullet.GetComponent<Rigidbody>().AddRelativeForce(relativePos * 50);
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
