using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public Button menuButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Title")
        {
            menuButton.onClick.AddListener(MenuButtonFunction);//adds a listener for when you click the button
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && SceneManager.GetActiveScene().name == "Title")
        {
            //will later have a fade out effect
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

            
        }
    }

    void MenuButtonFunction()
    {
        if(menuButton.name == "Start")
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
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
