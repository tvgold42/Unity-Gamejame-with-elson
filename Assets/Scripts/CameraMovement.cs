using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerPosition;
    public Transform myPosition;

    public Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myPosition = GetComponent<Transform>();
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        myRB.freezeRotation = true;
        myPosition.position = playerPosition.position;
    }
}
