using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private BannerView bannerAd;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    public static AdManager instance;

    private int playedLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();

    }

    /*INTERSTIAL ADS*/
    #region INTERSTIAL
    public void RequestIntertial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";     //Test ID
        //string adUnitId = "ca-app-pub-4762392528800851/3847810701";     //Gercek ID


        if (this.interstitialAd != null)
            this.interstitialAd.Destroy();

        this.interstitialAd = new InterstitialAd(adUnitId);

        this.interstitialAd.LoadAd(this.CreateAdRequest());

        this.interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;

        this.interstitialAd.OnAdClosed += InterstitialAd_OnAdClosed;
    }

    private void InterstitialAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestIntertial();
    }

    private void InterstitialAd_OnAdClosed(object sender, EventArgs e)
    {
        PlayerPrefs.SetInt("AdShowCount", 0);
        GameObject.FindObjectOfType<GameManager>().adShowCount = 0;
    }

    public void ShowIntertial()
    {
        if (this.interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }

        else
        {
            print("interstitialAd not loaded yet.");
        }
    }
    #endregion
}
