using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static int playerScore;

    public static int highScore1 = 0;
    public static int highScore2 = 0;
    public static int highScore3 = 0;
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

        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            highText.text = "Score: " + playerScore;

            if (timeToUpdate <= 0 && updateRecords == false)
            {
                updateRecords = true;
                //check if record broken, if yes, move all records down the list
                if (playerScore >= highScore1)
                {
                    highScore3 = highScore2;
                    highScore2 = highScore1;
                    highScore1 = playerScore;
                }
                else if (playerScore >= highScore2)
                {
                    highScore3 = highScore2;
                    highScore2 = playerScore;
                }
                else if (playerScore >= highScore3)
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
        PlayerPrefs.SetInt("Record1", highScore1);
        PlayerPrefs.SetInt("Record2", highScore2);
        PlayerPrefs.SetInt("Record3", highScore3);
    }

    public void Load()
    {
        //check if record exists, then load it
        if (PlayerPrefs.HasKey("Record1"))
        { highScore1 = PlayerPrefs.GetInt("Record1"); }
        if (PlayerPrefs.HasKey("Record2"))
        { highScore2 = PlayerPrefs.GetInt("Record2"); }
        if (PlayerPrefs.HasKey("Record3"))
        { highScore3 = PlayerPrefs.GetInt("Record3"); }
    }

    public void Delete()
    {
        PlayerPrefs.DeleteKey("Record1");
        PlayerPrefs.DeleteKey("Record2");
        PlayerPrefs.DeleteKey("Record3");
    }
}
