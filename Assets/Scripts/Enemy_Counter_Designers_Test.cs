using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Enemy_Counter_Designers_Test : MonoBehaviour
{
    public static float enemyCount = 0;
    public float publicEnemyCount = 0;
    public static float waveCount = 1;
    private Text badGuyLeft;
    public float levelWidth;
    public float levelHeight;
    public float timeBetweenSpawn;

    public float timeBetweenWaves;
    public bool waveComplete = false;

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
        badGuyLeft.text = "Enemies left: " + (totalInWave - totalKilledSoFar);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
