using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordTable : MonoBehaviour
{
    public Text recordText;

    // Start is called before the first frame update
    void Start()
    {
        recordText = GetComponent<Text>();
        if (gameObject.name == "Record1")
        {
            recordText.text = HighScore.highScore1.ToString();
        }
        if (gameObject.name == "Record2")
        {
            recordText.text = HighScore.highScore2.ToString();
        }
        if (gameObject.name == "Record3")
        {
            recordText.text = HighScore.highScore3.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
