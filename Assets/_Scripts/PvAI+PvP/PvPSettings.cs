using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PvPSettings : MonoBehaviour
{
    [Header("PvP")]
    public Text pvpTimeText;
    public Text goalAmountText;
    [Header("PvAi")]
    public Text pvpTimeTextAi;
    public Text goalAmountTextAi;

    private int pvpTime;
    private int goalAmount;

    #region Time

    private int PvpTime
    {
        get { return pvpTime; }
        set
        {
            pvpTime = value;

            if (value > 5)
            {
                pvpTime = 5;
            }

            if (value < 1)
            {
                pvpTime = 1;
            }
        }
    }

    public void IncreasePvPTime()
    {
        PvpTime++;
        pvpTimeText.text = pvpTime.ToString();
        pvpTimeTextAi.text = pvpTime.ToString();
        PlayerPrefs.SetInt("PvPTime", PvpTime);
    }

    public void DecreasePvPTime()
    {
        PvpTime--;
        pvpTimeText.text = PvpTime.ToString();
        pvpTimeTextAi.text = PvpTime.ToString();
        PlayerPrefs.SetInt("PvPTime", PvpTime);
    }
    #endregion

    #region GoalAmount
    private int GoalAmount
    {
        get { return goalAmount; }
        set
        {
            goalAmount = value;
            if (value > 10)
            {
                goalAmount = 10;
            }
            if (value < 1)
            {
                goalAmount = 1;
            }
        }
    }

    public void IncreasePvPGoalAmount()
    {
        GoalAmount++;
        goalAmountText.text = GoalAmount.ToString();
        goalAmountTextAi.text = GoalAmount.ToString();
        PlayerPrefs.SetInt("PvPGoalAmount", GoalAmount);
    }

    public void DecreasePvPGoalAmount()
    {
        GoalAmount--;
        goalAmountText.text = GoalAmount.ToString();
        goalAmountTextAi.text = GoalAmount.ToString();
        PlayerPrefs.SetInt("PvPGoalAmount", GoalAmount);
    }
    #endregion

    private void OnEnable()
    {
        PvpTime = PlayerPrefs.GetInt("PvPTime", 3);
        GoalAmount = PlayerPrefs.GetInt("PvPGoalAmount", 5);

        pvpTimeText.text = PvpTime.ToString();
        pvpTimeTextAi.text = PvpTime.ToString();
        goalAmountText.text = GoalAmount.ToString();
        goalAmountTextAi.text = GoalAmount.ToString();
    }

}
