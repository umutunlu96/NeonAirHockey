using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Settings")]
    public int maxLevel;
    private bool isEscape;

    [Header("Panels")]
    public GameObject main;
    public GameObject levelSelection;
    public GameObject championship;
    public GameObject playerVsAi;
    public GameObject playerVsPlayer;
    public GameObject about;
    public GameObject DifficultyTogles;

    private void Start()
    {
        DifficultyTogles.transform.GetChild((int)AiSettings.Difficulty).GetComponent<Toggle>().isOn = true;
    }

    private void Update()
    {
        CloseApplication();
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
                isEscape = true;
                closePanelGoMain();
                if (!IsInvoking("DisableDoubleClick"))
                    Invoke("DisableDoubleClick", 0.3f);
            }
        }
    }

    void DisableDoubleClick()
    {
        isEscape = false;
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetInt("Level") >= maxLevel)
            SceneManager.LoadScene(maxLevel);
        else
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1));
    }

    public void PlayCanivalMode()
    {
        SceneManager.LoadScene("Carnival");
        PlayerPrefs.SetInt("PvPAdShowCount", PlayerPrefs.GetInt("PvPAdShowCount") + 1);
    }

    public void PlayPlayerVsPlayer()
    {
        SceneManager.LoadScene("PlayerVsPlayerNew");
        PlayerPrefs.SetInt("PvPAdShowCount", PlayerPrefs.GetInt("PvPAdShowCount") + 1);
    }

    public void PlayVsAi()
    {
        SceneManager.LoadScene("PlayerVsAiNew");
        PlayerPrefs.SetInt("PvPAdShowCount", PlayerPrefs.GetInt("PvPAdShowCount") + 1);
    }

    private void closePanelGoMain()
    {
        main.SetActive(true);
        levelSelection.SetActive(false);
        championship.SetActive(false);
        playerVsAi.SetActive(false);
        playerVsPlayer.SetActive(false);
        about.SetActive(false);
    }

    public void MoreApps()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8187470632350050639&hl=tr&gl=US");
    }

    #region Difficulty
    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
        {
            AiSettings.Difficulty = AiSettings.Difficulties.Easy;
        }
    }

    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
        {
            AiSettings.Difficulty = AiSettings.Difficulties.Medium;
        }
    }

    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
        {
            AiSettings.Difficulty = AiSettings.Difficulties.Hard;
        }
    }

    #endregion

}
