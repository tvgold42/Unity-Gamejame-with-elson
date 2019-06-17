using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCell : MonoBehaviour
{
    public Animator bloodAnim;
    public float timeToStart = 1;

    // Start is called before the first frame update
    void Start()
    {
        bloodAnim = GetComponent<Animator>();
        bloodAnim.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToStart >= 0)
        {
            timeToStart -= Time.deltaTime;
        }
        if (timeToStart <= 0)
        {
            bloodAnim.speed = Random.Range(0.8f, 1.3f);
        }
    }
}
