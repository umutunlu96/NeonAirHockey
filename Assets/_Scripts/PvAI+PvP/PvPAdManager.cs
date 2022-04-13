using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PvPAdManager : MonoBehaviour
{
    public static PvPAdManager instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("ADS")]
    private bool SHOWADS = true;
    public int pvpAdShowCount; //Ads
    public bool adShow; //Ads

    void Start()
    {
        AdCheck(SHOWADS);
    }

    private void AdCheck(bool showAds)
    {
        if (showAds)
        {
            pvpAdShowCount = PlayerPrefs.GetInt("PvPAdShowCount", 0);

            if (pvpAdShowCount > 2)
            {
                AdManager.instance.RequestIntertialPvP();
                adShow = true;
            }
        }
    }

    public void AdCheck()
    {
        pvpAdShowCount = PlayerPrefs.GetInt("PvPAdShowCount", 0);

        if (pvpAdShowCount >= 2)
        {
            AdManager.instance.RequestIntertialPvP();
            adShow = true;
        }
    }



    public void ShowAd()
    {
        AdManager.instance.ShowIntertialPvP();
        this.adShow = false;
    }
}
