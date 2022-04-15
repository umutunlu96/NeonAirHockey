using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float gameDuration;

    private bool gameEnd = false;
    private bool isEscape = false;

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
        if (!gameEnd && gameDuration <= 0)
        {
            gameEnd = true;
            print("GameController GameEnnd");
            UIManager.Instance.ShowRestartCanvas(CheckWhoWin());
            PvPAdManager.instance.ShowAd();
        }

        CloseApplication();
    }

    public void ResetGame()
    {
        gameDuration = PlayerPrefs.GetInt("PvPTime") * 60;
        print("GameController ResetGame");
        gameEnd = false;
    }

    void CloseApplication()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscape)
            {
                Application.Quit();
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