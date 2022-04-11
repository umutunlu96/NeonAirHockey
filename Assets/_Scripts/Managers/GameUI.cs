using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    private GameManager gameManager;
    private int startBB;
    private Canvas canvas;

    [Header("PausedMenu")]
    public GameObject PausedMenu;
    public Image soundOff;
    public Image vibrateOff;

    [Header("Win Screen")]
    public GameObject winPanel;
    public Text goodJobText;
    public Image star1, star2, star3;
    public Sprite shineStar, darkStar;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    private bool isMuted;

    void Awake()
    {
        instance = this;
        gameManager = FindObjectOfType<GameManager>();
        canvas = transform.GetComponent<Canvas>();
    }

    void Start()
    {
        canvas.worldCamera = Camera.main;
        isMuted = PlayerPrefs.GetInt("Muted") == 1;
        SoundManager.instance.audioSource.mute = isMuted;
        soundOff.gameObject.SetActive(isMuted);
        vibrateOff.gameObject.SetActive(gameManager.isVibrating);
    }

    public void GameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void WinScreen()
    {
        winPanel.SetActive(true);

        //if (gameManager.shootAmount >= startBB)
        //{
        //    goodJobText.text = "FANTASTIC!";
        //    StartCoroutine(Stars(3));
        //}
        //else if (gameManager.shootAmount >= startBB - (gameManager.shootAmount / 2))
        //{
        //    goodJobText.text = "AWESOME!";
        //    StartCoroutine(Stars(2));
        //}
        //else if (gameManager.shootAmount > 0)
        //{
        //    goodJobText.text = "WELL DONE!";
        //    StartCoroutine(Stars(1));
        //}
        //else
        //{
        //    goodJobText.text = "GOOD!";
        //    StartCoroutine(Stars(0));
        //}
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
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MuteToggle()
    {
        isMuted = !isMuted;
        SoundManager.instance.audioSource.mute = isMuted;
        soundOff.gameObject.SetActive(isMuted);
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

    public void VibrationToggle()
    {
        gameManager.isVibrating = !gameManager.isVibrating;
        vibrateOff.gameObject.SetActive(gameManager.isVibrating);
        PlayerPrefs.SetInt("Vibrate", gameManager.isVibrating ? 1 : 0);
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
