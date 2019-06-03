using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_BadGuyManager : MonoBehaviour
{
    public int remainingBadGuys;
    public int badGuysToSpawn;
    public bool spawnBadGuys = true;
    public float badGuySpawnTimer;
    int spawnLocation;

    public GameObject BadGuy;
    public void BadGuyDestroid()
    {
        remainingBadGuys--;

        if (remainingBadGuys <= 0)
        {
            spawnBadGuys = true;
            badGuysToSpawn++;
        }
    }

    private void Update()
    {
        if (spawnBadGuys)
        {
            //timer
            badGuySpawnTimer += Time.deltaTime;

            //if more than 5 secons
            if (badGuySpawnTimer > 3)
            {
                //reset timer
                badGuySpawnTimer = 0;

                spawnLocation = Random.Range(0, 5);
                //spawn bad guy here
                Instantiate(BadGuy, this.transform.position, Quaternion.identity, this.gameObject.transform.GetChild(spawnLocation).gameObject.transform;

                //add to total bad guys in level
                remainingBadGuys++;

                //check if there are more bad guys than are needed to spawn
                if (remainingBadGuys >= badGuysToSpawn)
                {
                    //stop spawning bad guys
                    spawnBadGuys = false;
                }
            }
        }
    }
}
