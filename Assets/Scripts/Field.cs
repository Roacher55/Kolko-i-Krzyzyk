using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject square;
    public GameObject circle;
    public bool free = false;
    public int playerPoint = 0;
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    void OnMouseEnter()
    {
        if(free == false)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if(free == true)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        
    }
    private void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = Color.blue;
    }
    private void OnMouseDown()
    {
        SetFigureOnField();
    }

    void TurnObjectVisual()
    {
        GameHelper.turnCircle.SetActive(!GameHelper.turnCircle.activeSelf);
        GameHelper.turnSquare.SetActive(!GameHelper.turnSquare.activeSelf);
    }
    void SetFigureOnField()
    {
        if (free == false)
        {
            if (GameHelper.playerTurn == 1)
            {
                Instantiate(circle, new Vector3(transform.position.x, transform.position.y, -1.5f), Quaternion.identity);
                TurnObjectVisual();
                playerPoint = 1;
                GameHelper.playerTurn = 2;
            }
            else if (GameHelper.playerTurn == 2)
            {
                Instantiate(square, new Vector3(transform.position.x, transform.position.y, -1.5f), Quaternion.identity);
                TurnObjectVisual();
                playerPoint = 2;
                GameHelper.playerTurn = 1;
            }
            free = true;
        }
    }
}
