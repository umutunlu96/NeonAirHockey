using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    private GameManager gameManager;
    private int startBB;

    [Header("Win Screen")]
    public Text goodJobText;
    public GameObject winPanel;
    public Image star1, star2, star3;
    public Sprite shineStar, darkStar;

    [Header("Game Over")]
    public GameObject gameOverPanel;


    void Awake()
    {
        instance = this;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        startBB = gameManager.shootAmount;
    }

    public void GameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void WinScreen()
    {
        winPanel.SetActive(true);

        if (gameManager.shootAmount >= startBB)
        {
            goodJobText.text = "FANTASTIC!";
            StartCoroutine(Stars(3));
        }
        else if (gameManager.shootAmount >= startBB - (gameManager.shootAmount / 2))
        {
            goodJobText.text = "AWESOME!";
            StartCoroutine(Stars(2));
        }
        else if (gameManager.shootAmount > 0)
        {
            goodJobText.text = "WELL DONE!";
            StartCoroutine(Stars(1));
        }
        else
        {
            goodJobText.text = "GOOD!";
            StartCoroutine(Stars(0));
        }
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
