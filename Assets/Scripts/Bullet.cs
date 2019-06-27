using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife;
    public float totalBulletLife;

    // Update is called once per frame
    void Update()
    {
        //destory bullet if it exists for too long
       bulletLife += Time.deltaTime;
        if (bulletLife >= totalBulletLife && gameObject.tag != "enemy")
        {
            Destroy(gameObject);
        }

        if (bulletLife >= 2 && gameObject.tag == "enemy")
        {
            Destroy(gameObject);
        }

        //locks y position
        transform.position = new Vector3(transform.position.x, -4, transform.position.z);


    }

    void OnCollisionEnter(Collision other)
    {
        //make enemy bullets not bounce against everything
        if (gameObject.tag == "enemy" &&  other.gameObject.name != "enemyShoot(Clone)")
        {
            Destroy(gameObject);
        }
    }

}
