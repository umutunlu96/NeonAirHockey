using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float gameDuration;

    private bool gameEnd = false;

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
        if(!gameEnd)
            gameDuration -= Time.deltaTime;
        if (gameDuration <= 0)
        {
            gameEnd = true;
            UIManager.Instance.ShowRestartCanvas(CheckWhoWin());
        }
    }

    public void ResetGame()
    {
        gameDuration = PlayerPrefs.GetInt("PvPTime") * 60;
        gameEnd = false;
    }
}