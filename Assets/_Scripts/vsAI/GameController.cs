using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float gameDuration;
    private int goalAmount;

    public bool gameEnd = false;

    private ScoreScript scoreScript;

    void Start()
    {
        scoreScript = GetComponent<ScoreScript>();

        #region Time
        switch (GameSettings.Time)
        {
            case GameSettings.TimeSettings.one:
                gameDuration = 1;
                break;
            case GameSettings.TimeSettings.two:
                gameDuration = 2;
                break;
            case GameSettings.TimeSettings.three:
                gameDuration = 3;
                break;
            case GameSettings.TimeSettings.four:
                gameDuration = 4;
                break;
            case GameSettings.TimeSettings.five:
                gameDuration = 5;
                break;
            default:
                gameDuration = 3;
                break;
        }
        #endregion


        #region GoalAmount
        switch (GameSettings.GoalAmount)
        {
            case GameSettings.GoalAmountSettings.one:
                goalAmount = 1;
                break;
            case GameSettings.GoalAmountSettings.two:
                goalAmount = 2;
                break;
            case GameSettings.GoalAmountSettings.three:
                goalAmount = 3;
                break;
            case GameSettings.GoalAmountSettings.four:
                goalAmount = 4;
                break;
            case GameSettings.GoalAmountSettings.five:
                goalAmount = 5;
                break;
            case GameSettings.GoalAmountSettings.six:
                goalAmount = 6;
                break;
            case GameSettings.GoalAmountSettings.seven:
                goalAmount = 7;
                break;
            case GameSettings.GoalAmountSettings.eight:
                goalAmount = 8;
                break;
            case GameSettings.GoalAmountSettings.nine:
                goalAmount = 9;
                break;
            case GameSettings.GoalAmountSettings.ten:
                goalAmount = 10;
                break;
            default:
                goalAmount = 5;
                break;
        }
        #endregion

        scoreScript.maxScore = goalAmount;
    }

    private bool CheckWhoWin()
    {
        if (scoreScript.AiScore > scoreScript.PlayerScore)
            return true;
        else
            return false;
    }

    private void Update()
    {
        if(!gameEnd)
            gameDuration -= Time.deltaTime;
        if (gameDuration <= 0)
        {
            gameEnd = true;
            StartCoroutine(scoreScript.DelayRestartCanvas(CheckWhoWin()));
        }
    }


}