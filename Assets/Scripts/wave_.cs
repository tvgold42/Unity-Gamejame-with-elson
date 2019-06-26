using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class wave_ : MonoBehaviour
{

    public static int wave;
    private int privatewave;
    public Text waveText;
    private EnemyCounter enemyCounter;
    private Enemy_Counter_Designers_Test designer_enemyCounter;
    // Start is called before the first frame update
    void Start()
    {
        waveText = GetComponent<Text>();
        enemyCounter = GameObject.Find("EnemySpawnHandler").GetComponent<EnemyCounter>();
        designer_enemyCounter = GameObject.Find("EnemySpawnHandler").GetComponent<Enemy_Counter_Designers_Test>();
    }

    // Update is called once per frame
    void Update()
    {
        privatewave = wave;
        if (gameObject.name == "Wave Counter")
        {
            if (designer_enemyCounter.waveCount == 1)
            {
                waveText.text = "Wave: 3";

            }
            else
            if (designer_enemyCounter.waveCount == 2)
            {
                waveText.text = "Wave: 2";

            }
            else
            {
                waveText.text = "Final Wave";
            }

            
        }
        if (gameObject.name == "Bad Guys Left")
        {
            waveText.text = "Enemies Left: " + (designer_enemyCounter.totalInWave - designer_enemyCounter.totalKilledSoFar);
        }
        if (gameObject.name == "Health")
        {
            waveText.text = "Health: " + Player.playerHealth;
        }
    }
}
