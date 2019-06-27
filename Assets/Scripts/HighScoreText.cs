using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{
    public Text textRender;

 
    void Start()
    {
        textRender = GetComponent<Text>();

        if (HighScore.playerScore >= HighScore.highScore1)
        {
            textRender.text = "";
        }
    }

    void Update()
    {

    }
}
