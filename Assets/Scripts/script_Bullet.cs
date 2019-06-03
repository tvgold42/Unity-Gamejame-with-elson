using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_Bullet : MonoBehaviour
{
    float lifeTime;
    private void Start()
    {
        //pushes the bullet on spawn forwards
        this.GetComponent<Rigidbody2D>().AddForce(new Vector3(0,0,0.02f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name + " has entered");
        if (collision.tag == "BadGuy")
        {
            Destroy(collision.gameObject);
            GameObject.Find("BadGuyManager").GetComponent<script_BadGuyManager>().BadGuyDestroid();
            Destroy(this.gameObject);

        }
    }
}
