using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static float playerScore;

    public static float highScore1 = 99999;
    public static float highScore2 = 99999;
    public static float highScore3 = 99999;
    public float timeToUpdate = 0.5f;
    public bool updateRecords = false;

    public Text highText;

    // Start is called before the first frame update
    void Start()
    {
        highText = GetComponent<Text>();
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        timeToUpdate -= Time.deltaTime;


        if (SceneManager.GetActiveScene().name == "Victory")
        {
            highText.text = "Time: " + playerScore;

            if (timeToUpdate <= 0 && updateRecords == false)
            {
                updateRecords = true;
                //check if record broken, if yes, move all records down the list
                if (playerScore <= highScore1)
                {
                    highScore3 = highScore2;
                    highScore2 = highScore1;
                    highScore1 = playerScore;
                }
                else if (playerScore <= highScore2)
                {
                    highScore3 = highScore2;
                    highScore2 = playerScore;
                }
                else if (playerScore <= highScore3)
                {
                    highScore3 = playerScore;
                }
                Save();
            }
            
        }

        //update this scripts score with the players score from the main score script
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            playerScore = Score.score;
        }
    }

    public void Save()
    {
        //update save with new records
        PlayerPrefs.SetFloat("Record1", highScore1);
        PlayerPrefs.SetFloat("Record2", highScore2);
        PlayerPrefs.SetFloat("Record3", highScore3);
    }

    public void Load()
    {
        //check if record exists, then load it
        if (PlayerPrefs.HasKey("Record1"))
        { highScore1 = PlayerPrefs.GetFloat("Record1"); }
        if (PlayerPrefs.HasKey("Record2"))
        { highScore2 = PlayerPrefs.GetFloat("Record2"); }
        if (PlayerPrefs.HasKey("Record3"))
        { highScore3 = PlayerPrefs.GetFloat("Record3"); }
    }

    public void Delete()
    {
        PlayerPrefs.DeleteKey("Record1");
        PlayerPrefs.DeleteKey("Record2");
        PlayerPrefs.DeleteKey("Record3");
    }
}
