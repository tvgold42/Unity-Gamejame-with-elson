using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCell : MonoBehaviour
{
    public float startPosition = 50;
    public float endPosition = -50;
    public Rigidbody cellRB;
    public Transform cellPos;
    public SpriteRenderer cellSprite;
    public Sprite cellSprite1;
    public Sprite cellSprite2;
    public Sprite cellSprite3;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = 50;
        endPosition = -50;
        cellRB = GetComponent<Rigidbody>();
        cellPos = GetComponent<Transform>();
        cellSprite = GetComponent<SpriteRenderer>();
        cellPos.position = new Vector3(transform.position.x, Random.Range(-19, 20), Random.Range(20, 30));
        cellRB.AddForce(Random.Range(-400,-600), 0, 0);

        if (Random.Range(0,100) >= 70)
        {
            cellSprite.sprite = cellSprite1;
        }
        else if (Random.Range(0, 100) >= 60)
        {
            cellSprite.sprite = cellSprite2;
        }
        else if (Random.Range(0, 100) >= 50)
        {
            cellSprite.sprite = cellSprite3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cellPos.position.x <= endPosition)
        {
            cellPos.position = new Vector3(startPosition, Random.Range(-5, 5), Random.Range(20, 30));

            if (Random.Range(0, 100) >= 50)
            {
                cellSprite.sprite = cellSprite1;
            }
            else if (Random.Range(0, 100) >= 50)
            {
                cellSprite.sprite = cellSprite2;
            }
            else if (Random.Range(0, 100) >= 50)
            {
                cellSprite.sprite = cellSprite3;
            }

            cellRB.AddForce(Random.Range(-50, 50), 0, 0);
        }
    }
        




}
