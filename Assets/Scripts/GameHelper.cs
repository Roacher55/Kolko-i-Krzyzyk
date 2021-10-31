using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject field;
    public List<GameObject> ListField = new List<GameObject>();
    float startX = -2.5f;
    float startY = 2.5f;
    public static int playerTurn = 1;
    public  GameObject circle;
    public  GameObject square;
    public static GameObject turnSquare;
    public static GameObject turnCircle;
    private bool isRunning = false;
    bool color = false;
    public GameObject textTurn;
    public GameObject winText;
    private bool restartClick = false;
    private int pass = 0;
    




    void Start()
    {
        CreateFields();
    }

    // Update is called once per frame
    void Update()
    {
        TurnObjectMethod();
        StartCoroutine(ChangeColourText(textTurn));
        Win();
        Quiit();
    }

    void CreateFields()
    {
        float tempX = startX;
        float tempY = startY;
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                ListField.Add(Instantiate(field, new Vector3(tempX, tempY, -1), Quaternion.identity));
                tempX = tempX + Mathf.Abs(startX);               
            }
            tempY = tempY - Mathf.Abs(startY);
            tempX = startX;
        }
        TurnObjectStartVisual();
    }

    void TurnObjectMethod()
    {
        if(playerTurn == 1)
        {
          //  turnObject.GetComponent<SpriteRenderer>().sprite = circle.GetComponent<SpriteRenderer>().sprite;
        }
        else if(playerTurn == 2)
        {
         //   turnObject.GetComponent<SpriteRenderer>().sprite = square.GetComponent<SpriteRenderer>().sprite;
        }

        Debug.Log("!!!" + playerTurn);
    }

    void TurnObjectStartVisual()
    {
        turnCircle = Instantiate(circle, new Vector3(-7, -0.5f, -1.5f), Quaternion.identity);
        turnSquare = Instantiate(square, new Vector3(-7, -0.5f, -1.5f), Quaternion.identity);
        turnSquare.SetActive(false);
    }

    IEnumerator ChangeColourText(GameObject text)
    {
        if(!isRunning)
        {
            isRunning = true;
            yield return new WaitForSeconds(1);
            ChangeColour(textTurn);
            if(restartClick == true)
            {
                ChangeColour(winText);
            }
            isRunning = false;
        }
    }
    void ChangeColour(GameObject text)
    {
        if(text.GetComponent<TextColor>().conditionColour)
        {
            text.GetComponent<Text>().color = Color.black;
            text.GetComponent<TextColor>().conditionColour = !text.GetComponent<TextColor>().conditionColour;
        }
        else
        {
            text.GetComponent<Text>().color = Color.white;
            text.GetComponent<TextColor>().conditionColour = !text.GetComponent<TextColor>().conditionColour;
        }
    }

    void Win()
    {
        RestartGame();  
        WinCondition(ListField[0].GetComponent<Field>(), ListField[1].GetComponent<Field>(), ListField[2].GetComponent<Field>());
        WinCondition(ListField[3].GetComponent<Field>(), ListField[4].GetComponent<Field>(), ListField[5].GetComponent<Field>());
        WinCondition(ListField[6].GetComponent<Field>(), ListField[7].GetComponent<Field>(), ListField[8].GetComponent<Field>());
        WinCondition(ListField[0].GetComponent<Field>(), ListField[3].GetComponent<Field>(), ListField[6].GetComponent<Field>());
        WinCondition(ListField[1].GetComponent<Field>(), ListField[4].GetComponent<Field>(), ListField[7].GetComponent<Field>());
        WinCondition(ListField[2].GetComponent<Field>(), ListField[5].GetComponent<Field>(), ListField[8].GetComponent<Field>());
        WinCondition(ListField[0].GetComponent<Field>(), ListField[4].GetComponent<Field>(), ListField[8].GetComponent<Field>());
        WinCondition(ListField[2].GetComponent<Field>(), ListField[4].GetComponent<Field>(), ListField[6].GetComponent<Field>());
        Pass();
    }

    void WinCondition(Field a, Field b, Field c)
    {
        if(a.playerPoint != 0)
        { 
             if(a.playerPoint == b.playerPoint && b.playerPoint ==c.playerPoint)
             {
                WinVisual(a, b, c);
            }
        }
        
    }
    void WinVisual(Field a, Field b, Field c)
    {
        a.GetComponent<SpriteRenderer>().color = Color.yellow;
        b.GetComponent<SpriteRenderer>().color = Color.yellow;
        c.GetComponent<SpriteRenderer>().color = Color.yellow;
        winText.SetActive(true);
        restartClick = true;

    }

    void PassVisual()
    {
        winText.SetActive(true);
        restartClick = true;

    }

    void RestartGame()
    {
        if(restartClick)
        {
            if(Input.GetMouseButtonDown(0))
                {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        Debug.Log("Wim");
    }

    void Pass()
    {
        foreach(GameObject f in ListField)
        {
            if (f.GetComponent<Field>().playerPoint != 0)
            {
                pass++;
            }
        }
        if(pass == ListField.Count)
        {
            PassVisual();
        }
        else
        {
            pass = 0;
        }
    }
    void Quiit()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}

