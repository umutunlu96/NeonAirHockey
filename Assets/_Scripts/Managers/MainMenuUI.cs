using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject play;
    public GameObject levelSelection;
    public int maxLevel;
    private bool isEscape;
    public GameObject DifficultyTogles;

    private void Start()
    {
        PlayerPrefs.SetInt("AdShowCount", 0);
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
                closeLevelSelect();
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
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1) + 2);
    }

    public void PlayPlayerVsPlayer()
    {
        SceneManager.LoadScene("PlayerVsPlayer");
    }


    public void PlayVsAi()
    {
        SceneManager.LoadScene("PlayerVsAi");
    }

    private void closeLevelSelect()
    {
        play.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void SelectLevel()
    {
        play.SetActive(false);
        levelSelection.SetActive(true);
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


    #region PVP Settings


    #endregion

}
