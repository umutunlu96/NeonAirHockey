using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject CanvasPause;

    [Header("CanvasRestart")]
    public GameObject WinTxt;
    public GameObject LoseTxt;
    public GameObject DrawTxt;

    [Header("PausedMenu")]
    public Image soundOff;
    public Image vibrateOff;


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
        soundOff.gameObject.SetActive(PlayerPrefs.GetInt("Muted") == 1);
        vibrateOff.gameObject.SetActive(PlayerPrefs.GetInt("Vibrate", 1) == 1);
    }

    public void ShowRestartCanvas(float whoWin)
    {
        print("UIManagerShowRestartCanvas");
        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (whoWin == 1)
        {
            PlayWinSound();
            WinTxt.SetActive(true);
            LoseTxt.SetActive(false);
            DrawTxt.SetActive(false);

        }
        else if (whoWin == 2)
        {
            PlayLoseSound();
            WinTxt.SetActive(false);
            LoseTxt.SetActive(true);
            DrawTxt.SetActive(false);
        }
        else if (whoWin == 0)
        {
            PlayDrawSound();
            WinTxt.SetActive(false);
            LoseTxt.SetActive(false);
            DrawTxt.SetActive(true);
        }

        PvPAdManager.instance.ShowAd();
    }

    void Update()
    {
        if (CanvasPause.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        if (CanvasRestart.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        if (CanvasGame.activeInHierarchy)
        {
            Time.timeScale = 1;
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
        print("UICanvas Restart Game");
        PlayerPrefs.SetInt("PvPAdShowCount", PlayerPrefs.GetInt("PvPAdShowCount") + 1);

        PvPAdManager.instance.AdCheck();

        scoreScript.ResetScore();
        gameController.ResetGame();

        foreach (var obj in ResetableGameObjects)
        {
            obj.ResetPosition();
        }

        ResumeGame();
    }

    public void PauseGame()
    {
        print("UICanvas Pause Game");
        CanvasPause.SetActive(true);
        CanvasRestart.SetActive(false);
        CanvasGame.SetActive(false);
    }

    public void ResumeGame()
    {
        print("UI Canvas Resume Game");
        CanvasPause.SetActive(false);
        CanvasRestart.SetActive(false);
        CanvasGame.SetActive(true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        print("UICanvas GoToMenu");
        SceneManager.LoadScene("MainMenu Glow New");
    }

    public void MuteToggle()
    {
        EffectManager.instance.MuteToggle();
        soundOff.gameObject.SetActive(EffectManager.instance.isMuted);
    }

    public void VibrationToggle()
    {
        EffectManager.instance.VibrationToggle();
        vibrateOff.gameObject.SetActive(EffectManager.instance.isNotVibrating);
        FindObjectOfType<BallScript>().isVibrating = !(FindObjectOfType<BallScript>().isVibrating);
    }
}