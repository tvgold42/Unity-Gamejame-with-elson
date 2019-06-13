using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wave : MonoBehaviour
{
    public static int wave;
    private int privatewave;
    public Text waveText;
    private EnemyCounter enemyCounter;
    // Start is called before the first frame update
    void Start()
    {
        waveText = GetComponent<Text>();
        enemyCounter = GameObject.Find("EnemySpawnHandler").GetComponent<EnemyCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        privatewave = wave;
        waveText.text = "Wave: " + EnemyCounter.waveCount;
    }
}
