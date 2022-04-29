using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnavalManager : MonoBehaviour
{
    private float gameDuration;

    public bool gameEnd = false;
    private bool isEscape = false;

    private ScoreScript scoreScript;


    void Start()
    {
        scoreScript = GetComponent<ScoreScript>();
        gameDuration = PlayerPrefs.GetInt("PvPTime", 3) * 60;
    }

    private int CheckWhoWin()
    {
        if (scoreScript.PlayerScore > scoreScript.AiScore)
            return 1;
        else if (scoreScript.PlayerScore < scoreScript.AiScore)
            return 2;
        else
            return 0;
    }

    private void Update()
    {
        if (!gameEnd)
        {
            gameDuration -= Time.deltaTime;
        }
        if (!gameEnd && gameDuration <= 0)
        {
            gameEnd = true;
            print("GameController GameEnnd");
            UIManager.Instance.ShowRestartCanvas(CheckWhoWin());
        }

        CloseApplication();
    }

    void CloseApplication()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscape)
            {
                //Application.Quit();
                UIManager.Instance.ResumeGame();
            }
            else
            {
                UIManager.Instance.PauseGame();
                print("GameController Escape presseed");
                isEscape = true;
                if (!IsInvoking("DisableDoubleClick"))
                    Invoke("DisableDoubleClick", 0.3f);
            }
        }
    }

    void DisableDoubleClick()
    {
        isEscape = false;
    }
}
