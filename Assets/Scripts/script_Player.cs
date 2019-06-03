using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 CODE REFFERENCES
 [1] https://docs.unity3d.com/ScriptReference/RaycastHit-point.html
 [2] https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
     */

public class script_Player : MonoBehaviour
{
    
    public float speed;
    public GameObject bullet;
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        //uses add force to move the player arround, add force is used to get a dragged feel for the game
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed));

        //Fire bullet with mouse
        if (Input.GetMouseButtonDown(0))
        {
            //raycasts mouse position, taken from [1]
            RaycastHit hit;
            Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
            if (Physics.Raycast(ray, out hit))
            {
                //creates target gameobject at mouse click from the raycast above
                GameObject CurrentTarget = Instantiate(Target, new Vector3(ray.origin.x, ray.origin.y, 0), new Quaternion(0, 0,0,0));
                
                //Debugging tool, performs a raycast towards the target
                Debug.DrawLine(this.gameObject.transform.position, CurrentTarget.transform.position);

                RaycastHit2D raycast = Physics2D.Raycast(transform.position, (this.transform.position - CurrentTarget.transform.position));

                //creates bullet 5 units away from the player [2]
                GameObject bulletSpawned = Instantiate(bullet, this.transform.position, Quaternion.identity);
                bulletSpawned.transform.LookAt(CurrentTarget.transform  );
                bulletSpawned.GetComponent<Rigidbody2D>().AddForce(bulletSpawned.transform.forward * 20);


                Destroy(CurrentTarget);
            }


 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BadGuy")
        { Destroy(this.gameObject); }
    }

}
