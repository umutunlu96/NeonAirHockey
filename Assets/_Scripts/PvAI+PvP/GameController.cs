using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float gameDuration;

    private bool gameEnd = false;

    private ScoreScript scoreScript;

    public GameObject[] AwayPlayerColors;
    public GameObject[] AwayWallColors;
    public GameObject[] HomePlayerColors;
    public GameObject[] HomeWallColors;

    private int homeColorIndex;
    private int awayColorIndex;

    void Start()
    {
        scoreScript = GetComponent<ScoreScript>();
        gameDuration = PlayerPrefs.GetInt("PvPTime", 3) * 60;

        homeColorIndex = PlayerPrefs.GetInt("HomeColor", 0);
        awayColorIndex = PlayerPrefs.GetInt("AwayColor", 1);

        GetColors();
    }

    private void GetColors()
    {
        ChangeColor(HomePlayerColors, homeColorIndex);
        ChangeColor(HomeWallColors, homeColorIndex);

        ChangeColor(AwayPlayerColors, awayColorIndex);
        ChangeColor(AwayWallColors, awayColorIndex);
    }

    private void ChangeColor(GameObject[] Sides, int Color)
    {
        foreach (GameObject Side in Sides)
        {
            Side.SetActive(false);
        }
        Sides[Color].SetActive(true);
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