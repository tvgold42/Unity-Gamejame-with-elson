using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{
    public Text textRender;

    // Start is called before the first frame update
    void Start()
    {
        textRender = GetComponent<Text>();

        if (HighScore.playerScore >= HighScore.highScore1)
        {
            textRender.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
