using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public SpriteRenderer pauseRender;
    public bool paused = false;

    void Start()
    {
        pauseRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            //toggles pause
            paused = !paused;
        }

        if (paused == true)
        {
            pauseRender.enabled = true;
            Time.timeScale = 0;
        }
        if (paused == false)
        {
            pauseRender.enabled = false;
            Time.timeScale = 1;
        }
    }
}
