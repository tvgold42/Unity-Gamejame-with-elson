using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife;
    public float totalBulletLife;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log ("I Was Shot");
    }

    // Update is called once per frame
    void Update()
    {
        //destory bullet if it exists for too long
       this.bulletLife += Time.deltaTime;
        if (this.bulletLife >= totalBulletLife)
        {
            Destroy(this.gameObject);
        }

        //locks y position
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);


    }


}
