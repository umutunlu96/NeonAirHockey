using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    #endregion



    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject WinTxt;
    public GameObject LoseTxt;

    [Header("Audio")]
    public AudioClip wonGame;
    public AudioClip loseGame;

    private ScoreScript scoreScript;

    public List<IResettable> ResetableGameObjects = new List<IResettable>();

    public object SceneManeger { get; private set; }

    private void Start()
    {
        scoreScript = GameObject.FindObjectOfType<ScoreScript>();
    }

    public void ShowRestartCanvas(bool didAIWin)
    {
        Time.timeScale = 0;
        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (didAIWin)
        {
            PlayLoseSound();
            WinTxt.SetActive(false);
            LoseTxt.SetActive(true);
        }
        else
        {
            PlayWinSound();
            WinTxt.SetActive(true);
            LoseTxt.SetActive(false);
        }

    }

    private void PlayWinSound()
    {
        SoundManager.instance.PlaySoundFX(wonGame, .5f);
    }

    private void PlayLoseSound()
    {
        SoundManager.instance.PlaySoundFX(loseGame, .5f);
    }


    public void RestartGame()
    {
        Time.timeScale = 1;
        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);

        scoreScript.ResetScore();

        foreach (var obj in ResetableGameObjects)
        {
            obj.ResetPosition();
        }

    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
