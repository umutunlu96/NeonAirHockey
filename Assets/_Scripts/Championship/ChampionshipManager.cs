using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChampionshipManager : MonoBehaviour
{
    [Header("STATS")]
    public GameObject Easy;
    public GameObject Medium;
    public GameObject Hard;
    
    public Text LevelNumber;
    public Text Wins;
    public Text Losses;
    public Text WinStreak;
    public Text WinRate;

    private int levelNumber;
    private int wins;
    private int losses;
    private int winStreak;
    private float winRate;
    private float champTime;

    public float ChampTime
    {
        get { return champTime; }
        set { champTime = Mathf.Clamp(value, 0, 7); }
    }

    private void Start()
    {
        GetSavedData();
        SetText();
        DifficultyChange();
        TimeChange();
    }

    private void GetSavedData()
    {
        levelNumber = PlayerPrefs.GetInt("ChampLevelNumber", 1);
        wins = PlayerPrefs.GetInt("ChampWins", 0);
        losses = PlayerPrefs.GetInt("ChampLosses", 0);
        winStreak = PlayerPrefs.GetInt("ChampWinStreak", 0);
        winRate = PlayerPrefs.GetFloat("ChampWinRate", 0);
    }

    private void SetText()
    {
        LevelNumber.text = levelNumber.ToString();
        Wins.text = wins.ToString();
        Losses.text = losses.ToString();
        WinStreak.text = winStreak.ToString();
        WinRate.text = "%" + winRate.ToString();
    }

    private void DifficultyTextChange(string a)
    {
        switch (a)
        {
            case "E":
                Easy.SetActive(true);
                Medium.SetActive(false);
                Hard.SetActive(false);
                break;

            case "M":
                Easy.SetActive(false);
                Medium.SetActive(true);
                Hard.SetActive(false);
                break;
            case "H":
                Easy.SetActive(false);
                Medium.SetActive(false);
                Hard.SetActive(true);
                break;
        }
    }

    private void DifficultyChange()
    {
        levelNumber = PlayerPrefs.GetInt("ChampLevelNumber", 1);

        if (0 < levelNumber && levelNumber <= 5)
        {
            AiSettings.Difficulty = AiSettings.Difficulties.Easy;
            DifficultyTextChange("E");
        }
        if (5 < levelNumber && levelNumber <= 15)
        {
            AiSettings.Difficulty = AiSettings.Difficulties.Medium;
            DifficultyTextChange("M");
        }
        if (15 < levelNumber && levelNumber <= 30)
        {
            AiSettings.Difficulty = AiSettings.Difficulties.Hard;
            DifficultyTextChange("H");
        }
    }

    private void TimeChange()
    {
        
        if (0 < levelNumber && levelNumber <= 5)
        {
            ChampTime = levelNumber;
        }
        if (5 < levelNumber && levelNumber <= 15)
        {
            ChampTime = levelNumber - 5;
        }
        if (15 < levelNumber && levelNumber <= 30)
        {
            ChampTime = levelNumber - 10;
        }

        PlayerPrefs.SetFloat("ChampTime", ChampTime);
    }

    public void StartChampionship()
    {
        SceneManager.LoadScene("Championship");
    }

}
