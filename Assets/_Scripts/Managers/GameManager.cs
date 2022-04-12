using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool gameOver;



    [Header("Timer")]
    public float timer = 15;

    private int levelNumber;
    private Animator fadeAnim;
    private bool isEscape;
    private GameUI gameUI;

    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level", 1);
        gameUI = FindObjectOfType<GameUI>();

        //fadeAnim = GameObject.Find("Fade").GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        CloseApplication();

        if (!gameOver && timer <= 0)
        {
            //GameUI.instance.GameOverScreen();
            gameOver = true;
        }
    }

    public void CheckFinishState()
    {
        if (GameObject.FindGameObjectWithTag("Finish") == null)
        {

            StartCoroutine(NextLevel());

            //GameUI.instance.WinScreen();

            if (levelNumber == SceneManager.GetActiveScene().buildIndex)
                PlayerPrefs.SetInt("Level", levelNumber + 1);
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator FadeIn(int sceneIndex)
    {
        fadeAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex));
    }

    //public void NextLevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    //StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex + 1));
    //}

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
        //StartCoroutine(FadeIn(0));
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
                gameUI.PausedScreen();
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
