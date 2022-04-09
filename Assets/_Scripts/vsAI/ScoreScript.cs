using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public enum Score
    {
        AiScore,PlayerScore
    }

    public Text AiScoreText, PlayerScoreText;

    private int maxScore;
    private UIManager uiManager;

    #region Scores
    private int aiScore, playerScore;

    public int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == maxScore)
            {
                StartCoroutine(DelayRestartCanvas(1));
            }
        }
    }
    public int AiScore 
    {
        get { return aiScore; }
        set 
        {
            aiScore = value; 
            if (value == maxScore)
            {
                StartCoroutine(DelayRestartCanvas(2));
            }
        }
    }
    public IEnumerator DelayRestartCanvas(float whoWin)
    {
        yield return new WaitForSeconds(1);
        uiManager.ShowRestartCanvas(whoWin);
    }

    #endregion

    private void Awake()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        maxScore = PlayerPrefs.GetInt("PvPGoalAmount",5);
    }

    public void Increment(Score whichScore)
    {
        if (whichScore == Score.AiScore)
            AiScoreText.text = (++AiScore).ToString();
        else
            PlayerScoreText.text = (++PlayerScore).ToString();
    }

    public void ResetScore()
    {
        AiScore = PlayerScore = 0;
        AiScoreText.text = PlayerScoreText.text = "0";
    }
}
