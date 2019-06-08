using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_MobileMovement : MonoBehaviour
{
    //most of the code in this script was from https://www.youtube.com/watch?v=bp2PiFC9sSs last accessed 08/06/2019
    //rotation detection code was from https://docs.unity3d.com/ScriptReference/Quaternion.html


    public bool NotTestingForMobile;
    public float turningSpeed;
    private Rigidbody playerRB;

    public float forwardInput;
    public float acceleration;
    public float CurrentAcceleration;

    public bool moveForward;

    public GameObject bulletForPositionTesting;

    private void Awake()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && NotTestingForMobile)
        {
            Debug.Log("Bad Guy spawning has been disabled as a public variable, this NEEDS to be disabled on final release");
            Destroy(this.GetComponent<Script_MobileSwitchBulletActivator>());
        }

        playerRB = this.GetComponent<Rigidbody>();
        forwardInput = this.gameObject.GetComponent<Player>().acceleration;
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && this.gameObject.GetComponent<Player>().death != true)
        {
            Vector3 touchPosition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);

            
            if (touchPosition.x > 0.7)
            {
                turningSpeed = 2;
            }

            else
            if (touchPosition.x < 0.3)
            {
                turningSpeed = -2;
            }
            else {turningSpeed = 0; }


            if (touchPosition.y >= 0.5)
                moveForward = true;
            else
                moveForward = false;
            playerRB.AddTorque(0, turningSpeed, 0);

            if (moveForward)
            playerRB.AddRelativeForce(Vector2.up  * (acceleration));



        }
    }
}
