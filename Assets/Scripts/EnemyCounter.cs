﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    public static float enemyCount = 0;
    public float publicEnemyCount = 0;
    public static float waveCount = 1;
    public float enemiesToSpawn = 0;
    public float enemiesSpawned = 0;

    public float levelWidth;
    public float levelHeight;
    public float timeBetweenSpawn;

    public float timeBetweenWaves;
    public static bool waveComplete = false;

    //all enemy types
    public GameObject whiteEnemy1;
    public GameObject blackEnemy1;
    public GameObject enemy3HP;
    public GameObject enemy2HP;
    public GameObject enemy1HP;

    public GameObject enemyShoot;

    // Start is called before the first frame update
    void Start()
    {
        enemiesToSpawn = 40;
        timeBetweenSpawn = 0.5f;

        enemyCount = 0;
        publicEnemyCount = 0;
        waveCount = 1;
        enemiesSpawned = 0;

}

    // Update is called once per frame
    void Update()
    {
        publicEnemyCount = enemyCount;
        timeBetweenSpawn -= Time.deltaTime;

        if (waveComplete == false)
        {
            //spawn the waves enemies
            if (enemiesToSpawn > 0 && timeBetweenSpawn <= 0)
            {
                timeBetweenSpawn = 0.5f;
                enemiesToSpawn -= 1;

                

                //pick and spawn a random enemy
                if (Random.Range(0, 100) >= 50 && waveCount > 1)
                { Instantiate(enemy3HP, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); enemiesSpawned += 1; }
               // if (Random.Range(0, 100) >= 30)
              //  { Instantiate(enemy2HP, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); }
                if (Random.Range(0, 100) >= 60)
                { Instantiate(enemyShoot, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); enemiesSpawned += 1; }
               // if (Random.Range(0, 100) >= 60) //60
               // { Instantiate(whiteEnemy1, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); }
               // if (Random.Range(0, 100) >= 60) //60
              //  { Instantiate(blackEnemy1, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); }
                else { Instantiate(enemy1HP, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); enemiesSpawned += 1; }
            }
        }




        //winning a wave
        if (enemyCount <= 0 && enemiesToSpawn <= 0 && waveComplete == false)
        {
            Debug.Log("Wave Complete!!!");
            if (waveCount >= 5)
            {
                Debug.Log("GAME COMPLETE");
            }
            waveComplete = true;
            timeBetweenWaves = 5f;
        }

        //counting to next wave
        if (waveComplete == true && timeBetweenWaves >= 0 && waveCount < 5)
        {
            timeBetweenWaves -= Time.deltaTime;
        }

        if (waveComplete == true && timeBetweenWaves <= 0 && waveCount < 5)
        {
            if(waveCount >= 3)
            {
                SceneManager.LoadScene("Victory", LoadSceneMode.Single);
            }
            waveComplete = false;
            waveCount += 1;
            enemiesSpawned = 0;
            enemiesToSpawn = 40 + (waveCount * 10);
        }
    }
}
