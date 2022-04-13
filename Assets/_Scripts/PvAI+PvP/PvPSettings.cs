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

    public GameObject[] HomeColors;
    public GameObject[] AwayColors;
    private int homeColorIndex;
    private int awayColorIndex;



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

    #region Colors

    public int HomeColorIndex
    {
        get
        {
            return homeColorIndex;
        }
        set
        {
            homeColorIndex = value;
            if (homeColorIndex >= 3)
                homeColorIndex = 3;
            else if (homeColorIndex <= 0)
                homeColorIndex = 0;
        }
    }

    public int AwayColorIndex
    {
        get
        {
            return awayColorIndex;
        }
        set
        {
            awayColorIndex = value;
            if (awayColorIndex >= 3)
                awayColorIndex = 3;
            else if (awayColorIndex <= 0)
                awayColorIndex = 0;
        }
    }

    private void GetSavedColorIndex()
    {
        HomeColorIndex = PlayerPrefs.GetInt("HomeColor", 0);
        AwayColorIndex = PlayerPrefs.GetInt("AwayColor", 1);

        //Home
        ChangeColor(HomeColors, HomeColorIndex);

        //Away
        ChangeColor(AwayColors, AwayColorIndex);
    }

    public void ChangeHomeColorButton(int Index)
    {
        HomeColorIndex += Index;
        PlayerPrefs.SetInt("HomeColor", HomeColorIndex);
        ChangeColor(HomeColors, HomeColorIndex);
    }


    public void ChangeAwayColorButton(int Index)
    {
        AwayColorIndex += Index;
        PlayerPrefs.SetInt("AwayColor", AwayColorIndex);
        ChangeColor(AwayColors, AwayColorIndex);
    }


    private void ChangeColor(GameObject[] Sides, int Color)
    {
        foreach (GameObject Side in Sides)
        {
            Side.SetActive(false);
        }
        Sides[Color].SetActive(true);
    }

    #endregion



    private void OnEnable()
    {
        PvpTime = PlayerPrefs.GetInt("PvPTime", 3);
        GoalAmount = PlayerPrefs.GetInt("PvPGoalAmount", 5);

        GetSavedColorIndex();

        pvpTimeText.text = PvpTime.ToString();
        pvpTimeTextAi.text = PvpTime.ToString();
        goalAmountText.text = GoalAmount.ToString();
        goalAmountTextAi.text = GoalAmount.ToString();
    }
}
