using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    public SpriteRenderer textRender;

    // Start is called before the first frame update
    void Start()
    {
        textRender = GetComponent<SpriteRenderer>();

        if (HighScore.playerScore >= HighScore.highScore1)
        {
            textRender.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
