using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    private GameManager gameManager;
    private Canvas canvas;

    [Header("Timer")]
    public Text timerText;
    private float timer;

    [Header("PausedMenu")]
    public GameObject PausedMenu;
    public Image soundOff;
    public Image vibrateOff;

    [Header("Win Screen")]
    public GameObject winPanel;
    //public Text goodJobText;
    public Image star1, star2, star3;
    public Sprite shineStar, darkStar;

    [Header("Game Over")]
    public GameObject gameOverPanel;



    void Awake()
    {
        instance = this;
        gameManager = FindObjectOfType<GameManager>();
        canvas = transform.GetComponent<Canvas>();
    }

    void Start()
    {
        canvas.worldCamera = Camera.main;
        timer = gameManager.timer;
        
        soundOff.gameObject.SetActive(PlayerPrefs.GetInt("Muted") == 1);
        vibrateOff.gameObject.SetActive(PlayerPrefs.GetInt("Vibrate", 1) == 1);
    }

    private void Update()
    {
        UpdateTimerText();
    }



    private void UpdateTimerText()
    {
        timerText.text = gameManager.timer.ToString("0.0");
        if (gameManager.timer >= timer * 2 / 3)
        {
            timerText.color = Color.green;
        }
        else if (gameManager.timer < timer * 2 / 3 && gameManager.timer >= timer / 3)
        {
            timerText.color = Color.green + Color.red;
        }
        else if (gameManager.timer <= timer * 1 / 3)
        {
            timerText.color = Color.red;
        }
    }

    public void GameOverScreen()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        GameObject.FindObjectOfType<Hockey>().gameObject.SetActive(false);
    }

    public void WinScreen()
    {
        //Time.timeScale = 0;
        winPanel.SetActive(true);
        GameObject.FindObjectOfType<Hockey>().gameObject.SetActive(false);

        if (gameManager.timer >= gameManager.timer * 2 / 3)
        {
            //goodJobText.text = "FANTASTIC!";
            StartCoroutine(Stars(3));
        }
        else if (gameManager.timer < timer * 2 / 3 && gameManager.timer >= timer / 3)
        {
            //goodJobText.text = "AWESOME!";
            StartCoroutine(Stars(2));
        }
        else if (gameManager.timer <= timer * 1 / 3)
        {
            //goodJobText.text = "WELL DONE!";
            StartCoroutine(Stars(1));
        }
        else
        {
            //goodJobText.text = "GOOD!";
            StartCoroutine(Stars(0));
        }
    }

    public void PausedScreen()
    {
        Time.timeScale = 0;
        PausedMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PausedMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        gameManager.GoToMainMenu();
    }
    public void Restart()
    {
        gameManager.Restart();
    }

    public void NextLevel()
    {
        gameManager.NextLevel();
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
    }

    private IEnumerator Stars(int shineNumber)
    {
        yield return new WaitForSeconds(.5f);

        switch (shineNumber)
        {
            case 3:
                yield return new WaitForSeconds(.15f);
                star1.sprite = shineStar;
                yield return new WaitForSeconds(.15f);
                star2.sprite = shineStar;
                yield return new WaitForSeconds(.15f);
                star3.sprite = shineStar;
                break;
            case 2:
                yield return new WaitForSeconds(.15f);
                star1.sprite = shineStar;
                yield return new WaitForSeconds(.15f);
                star2.sprite = shineStar;
                star3.sprite = darkStar;
                break;
            case 1:
                yield return new WaitForSeconds(.15f);
                star1.sprite = shineStar;
                star2.sprite = darkStar;
                star3.sprite = darkStar;
                break;
            case 0:
                star1.sprite = darkStar;
                star2.sprite = darkStar;
                star3.sprite = darkStar;
                break;
        }
    }
}
