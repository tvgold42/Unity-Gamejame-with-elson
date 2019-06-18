using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float score;
    public double privateScore;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        privateScore += Time.deltaTime;

        if (EnemyCounter.enemyCount <= 0 && EnemyCounter.waveComplete == true && EnemyCounter.waveCount == 3)
        {
            privateScore -= Time.deltaTime;
        }

        privateScore = Math.Round(privateScore , 2);
        score = (float)privateScore;
        scoreText.text = "Time: " + privateScore.ToString();
    }
}
