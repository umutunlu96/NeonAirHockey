using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject play;
    public GameObject levelSelection;
    public int maxLevel;
    private bool isEscape;

    private void Start()
    {
        PlayerPrefs.SetInt("AdShowCount", 0);
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
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1));
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
}
