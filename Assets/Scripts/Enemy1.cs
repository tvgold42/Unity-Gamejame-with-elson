using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform playerPos;
    public Rigidbody enemyRB;
    private Transform targetPosition;

    //different enemies
    public GameObject enemy2HP;
    public GameObject enemy1HP;
    public GameObject newEnemy;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        //targetPosition = GameObject.FindGameObjectsWithTag("Player").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 relativePos = GameObject.Find("player").transform.position - gameObject.transform.position;
        enemyRB.AddForce(0.1f * relativePos);
        //prevents y movement and locks rotation
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.rotation = Quaternion.Euler(-90, 0, 0);

        //limiting max speed
        if (enemyRB.velocity.magnitude >= 1f)
        {
            enemyRB.velocity = enemyRB.velocity.normalized;
        }

        //move towards player
        // transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 0.01f);
        //Vector3 relativePos = playerPos.position - transform.position;
        //enemyRB.AddForce(1.1f * relativePos);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "friendlyBullet")
        {
            Debug.Log("killed enemy");
            Destroy(other.gameObject);

            //check tag and act accordingly
            if (gameObject.tag == "enemy1hp")
            {
                Destroy(gameObject);
            }

            if (gameObject.tag == "enemy2hp")
            {
                //spawn 2 1hp enemies then die
                newEnemy = Instantiate(enemy1HP, transform.position, transform.rotation);
                newEnemy.transform.position += new Vector3(Random.Range(0.1f, -0.1f), 0, Random.Range(0.1f, -0.1f));
                newEnemy.GetComponent<Rigidbody>().AddForce(Random.Range(400f, -400f), 0, Random.Range(400f, -400f));

                newEnemy = Instantiate(enemy1HP, transform.position, transform.rotation);
                newEnemy.transform.position += new Vector3(Random.Range(0.1f, -0.1f), 0, Random.Range(0.1f, -0.1f));
                newEnemy.GetComponent<Rigidbody>().AddForce(Random.Range(400f, -400f), 0, Random.Range(400f, -400f));

                Destroy(gameObject);
            }

            if (gameObject.tag == "enemy3hp")
            {
                //spawn 2 2hp enemies then die
                newEnemy = Instantiate(enemy2HP, transform.position, transform.rotation);
                newEnemy.GetComponent<Rigidbody>().AddForce(Random.Range(200f, -200f), 0, Random.Range(200f, -200f));

                newEnemy = Instantiate(enemy2HP, transform.position, transform.rotation);
                newEnemy.GetComponent<Rigidbody>().AddForce(Random.Range(200f, -200f), 0, Random.Range(200f, -200f));

                Destroy(gameObject);
            }
        }
    }
}
