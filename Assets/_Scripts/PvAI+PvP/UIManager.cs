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
    public GameObject DrawTxt;

    [Header("Audio")]
    public AudioClip wonGame;
    public AudioClip loseGame;
    public AudioClip drawGame;

    private ScoreScript scoreScript;
    private GameController gameController;

    public List<IResettable> ResetableGameObjects = new List<IResettable>();

    public object SceneManeger { get; private set; }

    private void Start()
    {
        scoreScript = GameObject.FindObjectOfType<ScoreScript>();
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    public void ShowRestartCanvas(float whoWin)
    {
        Time.timeScale = 0;
        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (whoWin == 1)
        {
            PlayWinSound();
            WinTxt.SetActive(true);
            LoseTxt.SetActive(false);
            DrawTxt.SetActive(false);

        }
        else if(whoWin == 2)
        {
            PlayLoseSound();
            WinTxt.SetActive(false);
            LoseTxt.SetActive(true);
            DrawTxt.SetActive(false);
        }
        else if (whoWin == 0)
        {
            PlayWinSound();
            WinTxt.SetActive(false);
            LoseTxt.SetActive(false);
            DrawTxt.SetActive(true);
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
    private void PlayDrawSound()
    {
        SoundManager.instance.PlaySoundFX(drawGame, .5f);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);

        scoreScript.ResetScore();
        gameController.ResetGame();

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
