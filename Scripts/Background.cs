using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform backgroundPos;


    // Start is called before the first frame update
    void Start()
    {
        backgroundPos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        backgroundPos.localEulerAngles += new Vector3(0, 0, 0.03f);
    }
}
