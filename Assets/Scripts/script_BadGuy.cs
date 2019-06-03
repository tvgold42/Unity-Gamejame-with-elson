using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 CODE REFFERENCES
 [1] https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
     
     */

public class script_BadGuy : MonoBehaviour
{
    GameObject player;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        // finds player
        player = GameObject.Find("GoodGuy");
        //sets itself to have a random speed
        speed = Random.Range(0.01f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        //calculates how far it moves per frame based off of the speed set in start and time between each frame, this line is called "Actual Step" on the documentation [1]
        float turningSpeed = 3 * Time.deltaTime;
        Vector2 targetDirection = player.transform.position - transform.position;
        Vector2 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turningSpeed, 0.0f);
        //not sure what this next line does, but is required according to the documentation [1]
        transform.rotation = Quaternion.LookRotation(newDirection);

        //pushes it towards the player
        this.transform.Translate(Vector3.forward * speed);
    }
}
