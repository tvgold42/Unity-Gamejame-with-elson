using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public Button menuButton;
    public AudioSource menuAudio;
    public static bool AudioBegin = false;


    void Awake()
    {
        if ( gameObject.name == "AudioHandler")
        {
            menuAudio = GetComponent<AudioSource>();
        }
        if (AudioBegin == false && gameObject.name == "AudioHandler")
        {
            menuAudio.Play();
            AudioBegin = true;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        if (gameObject.name != "logo" && gameObject.name != "AudioHandler")
        {
            menuButton.onClick.AddListener(MenuButtonFunction);//adds a listener for when you click the button
        }


    }


    void Update()
    {
        if (Input.GetKeyDown("space") && SceneManager.GetActiveScene().name == "Title")
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            AudioBegin = false;
            menuAudio.Stop();
        }
    }

    void MenuButtonFunction()
    {
        //go to scene depending on what button clicked

        if(menuButton.name == "Start")
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
        if (menuButton.name == "Play")
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        if (menuButton.name == "HighScore")
        {
            SceneManager.LoadScene("HighScores", LoadSceneMode.Single);
        }
        if (menuButton.name == "HowToPlay")
        {
            SceneManager.LoadScene("HowToPlay", LoadSceneMode.Single);
        }
        if (menuButton.name == "Quit")
        {
            Application.Quit();
            Debug.Log("quit");
        }

        //back to main menu
        if (menuButton.name == "Back")
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
