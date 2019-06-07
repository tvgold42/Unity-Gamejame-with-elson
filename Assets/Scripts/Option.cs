using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public SpriteRenderer optionSprite;
    public Sprite white;
    public Sprite black;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        optionSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Animator>().GetBool("White") == true)
        {
            optionSprite.sprite = white;
        }
        if (player.GetComponent<Animator>().GetBool("White") == false)
        {
            optionSprite.sprite = black;
        }
    }
}
