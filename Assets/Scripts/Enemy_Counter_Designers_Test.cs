using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Enemy_Counter_Designers_Test : MonoBehaviour
{
    public static float enemyCount = 0;
    public float publicEnemyCount = 0;
    public float waveCount = 1;
    private Text badGuyLeft;
    public float levelWidth;
    public float levelHeight;
    public float timeBetweenSpawn;

    public float timeBetweenWaves;
    public bool waveComplete = false;
    public bool processedWaveComplete = false;
    public bool designersTestingStuff;
    public float desingersTestingStuffWaveStart;

    //all enemy types

    public GameObject trogen;
    public GameObject shooters;
    public GameObject swarm;

    public GameObject enemyShoot;
    public int swarmToSpawn;
    public int swarmSpawned;
    public int trogenToSpawn;
    public int trogenSpawned;
    public int shootersToSpawn;
    public int shootersSpawned;
    public bool finishedSpawning;
    public int totalInWave;
    public int totalKilledSoFar;
    // Start is called before the first frame update
    void Start()
    {
        badGuyLeft = GameObject.Find("Bad Guys Left").GetComponent<Text>();
        timeBetweenSpawn = 0.5f;
        enemyCount = 0;
        publicEnemyCount = 0;
        waveCount = 1;
        updateWaveTotal();
        Debug.Log("you are currently on level " + SceneManager.GetActiveScene().name);

        if (!designersTestingStuff)
        {
        swarmToSpawn = 12;
        shootersToSpawn = 8;
        trogenToSpawn = 0;
        }
        updateWaveTotal();
    }
    
    void updateWaveTotal()
    {
        totalKilledSoFar = 0;
        totalInWave = swarmToSpawn + (shootersToSpawn * 3) + (trogenToSpawn * 7);
    }

    // Update is called once per frame
    void Update()
    {
        publicEnemyCount = enemyCount;
        timeBetweenSpawn -= Time.deltaTime;
        if (totalKilledSoFar == totalInWave && processedWaveComplete == false)
        {
            Debug.Log("Wave Finished");
            waveComplete = true;
            timeBetweenWaves = 20;
            processedWaveComplete = true;
        }
        if (waveComplete == false)
        {
            //spawn the waves enemies
            if (timeBetweenSpawn <= 0)
            {
                timeBetweenSpawn = 0.5f;

                //pick and spawn a random enemy
                if (Random.Range(0, 100) >= 60 && trogenToSpawn > trogenSpawned)
                { Instantiate(trogen, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); trogenSpawned++;}
                if (Random.Range(0, 100) >= 30 && shootersToSpawn > shootersSpawned)
                { Instantiate(shooters, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); shootersSpawned++;}
                if (swarmToSpawn > swarmSpawned)
                { Instantiate(swarm, new Vector3(Random.Range(levelWidth, -levelWidth), 0, Random.Range(levelHeight, -levelHeight)), transform.rotation); swarmSpawned++;}
               
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
            processedWaveComplete = false;

            resetSpawnedCounter();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            if (waveCount == 2)
            {
                swarmToSpawn = 12;
                shootersToSpawn = 9;
                trogenToSpawn = 3;
                updateWaveTotal();
            }
            else if (waveCount == 3)
            {
                swarmToSpawn = 12;
                shootersToSpawn = 8;
                trogenToSpawn = 5;
                updateWaveTotal();

            }
            else if (waveCount > 3) { SceneManager.LoadScene("Victory"); }
        }
      
    }

    void resetSpawnedCounter()
    {
        swarmToSpawn = 0;
        shootersSpawned = 0;
        trogenSpawned = 0;
        totalKilledSoFar = 0;
    }

    public void badGuyDied()
    {
        totalKilledSoFar++;
        Debug.Log("BadGuy Killed, there are " + (totalInWave - totalKilledSoFar) + " left");
        badGuyLeft.text = "Enemies left: " + (totalInWave - totalKilledSoFar);

    }
}
