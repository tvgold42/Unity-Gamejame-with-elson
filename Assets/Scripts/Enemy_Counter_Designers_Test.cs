using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Counter_Designers_Test : MonoBehaviour
{
    public static float enemyCount = 0;
    public float publicEnemyCount = 0;
    public static float waveCount = 1;

    public float levelWidth;
    public float levelHeight;
    public float timeBetweenSpawn;

    public float timeBetweenWaves;
    public bool waveComplete = false;

    //all enemy types
    public GameObject whiteEnemy1;
    public GameObject blackEnemy1;
    public GameObject enemy3HP;
    public GameObject enemy2HP;
    public GameObject enemy1HP;

    public GameObject enemyShoot;

    public int oneHPToSpawn;
    public int oneHPSpawned;
    public int twoHPToSpawn;
    public int twoHPSpawned;
    public int threeHPToSpawn;
    public int threeHPSpawned;
    public int shootersHPToSpawn;
    public int shootersSpawned;
    public bool finishedSpawning;
    public int totalInWave;
    public int totalKilledSoFar;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawn = 0.5f;

        enemyCount = 0;
        publicEnemyCount = 0;
        waveCount = 1;
        updateWaveTotal();
    }
    
    void updateWaveTotal()
    {
        totalKilledSoFar = 0;
        totalInWave = oneHPToSpawn + (twoHPToSpawn * 3) + (threeHPToSpawn * 7) + (shootersHPToSpawn * 3);
    }

    // Update is called once per frame
    void Update()
    {
        publicEnemyCount = enemyCount;
        timeBetweenSpawn -= Time.deltaTime;
        if (totalKilledSoFar== totalInWave)
        {
            Debug.Log("Wave Finished");
            waveComplete = true;

        }
        if (waveComplete == false)
        {
            //spawn the waves enemies
            if (timeBetweenSpawn <= 0)
            {
                timeBetweenSpawn = 0.1f;

                //pick and spawn a random enemy
                if (Random.Range(0, 100) >= 60 && threeHPToSpawn > threeHPSpawned)
                { Instantiate(enemy3HP, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); threeHPSpawned++;}
                if (Random.Range(0, 100) >= 30 && twoHPToSpawn > twoHPSpawned)
                { Instantiate(enemy2HP, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); twoHPSpawned++;}
                if (Random.Range(0, 100) >= 80 && shootersHPToSpawn > shootersSpawned)
                { Instantiate(enemyShoot, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); shootersSpawned++;}
                //if (Random.Range(0, 100) >= 60) //60
                //{ Instantiate(whiteEnemy1, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); }
                //if (Random.Range(0, 100) >= 60) //60
                //{ Instantiate(blackEnemy1, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); }
                if (oneHPToSpawn > oneHPSpawned) { Instantiate(enemy1HP, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); oneHPSpawned++; }
            }
        }

        //counting to next wave
        if (waveComplete == true && timeBetweenWaves >= 0 && waveCount < 5)
        {
            timeBetweenWaves -= Time.deltaTime;
        }
        if (waveComplete == true && timeBetweenWaves <= 0 && waveCount < 5)
        {
            waveComplete = false;
            waveCount += 1;
        }
    }
}
