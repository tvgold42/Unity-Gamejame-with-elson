﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //destory bullet if it exists for too long
        bulletLife += Time.deltaTime;
        if (bulletLife >= 2)
        {
            Destroy(gameObject);
        }

        //locks y position
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);


    }


}
