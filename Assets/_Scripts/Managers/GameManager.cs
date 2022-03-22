using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int shootAmount;

    [HideInInspector]
    public bool gameOver;

    private int levelNumber;

    private Animator fadeAnim;

    private bool isEscape;



    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level", 1);

        //fadeAnim = GameObject.Find("Fade").GetComponent<Animator>();
    }

    void Update()
    {

        CloseApplication();


        if (!gameOver && FindObjectOfType<Hockey>().tryAmount <= 0)   //GameOverCondition
        {
            //GameUI.instance.GameOverScreen();
            gameOver = true;
        }

    }

    public void CheckFinishState()
    {
        if (GameObject.FindGameObjectWithTag("Finish") == null)
        {
            GameUI.instance.WinScreen();

            if (levelNumber == SceneManager.GetActiveScene().buildIndex)
                PlayerPrefs.SetInt("Level", levelNumber + 1);
        }
    }

    public void CheckBallCount()
    {

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

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex + 1));
    }

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
