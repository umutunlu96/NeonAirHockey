using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private BannerView bannerAd;
    private InterstitialAd interstitialAd;
    private InterstitialAd interstitialAdPvP;


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

    /*INTERSTIAL AD Level*/
    #region INTERSTIAL Level
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


    /*INTERSTIAL AD PVP + PVA*/
    #region INTERSTIAL PVP
    public void RequestIntertialPvP()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";     //Test ID
        //string adUnitId = "ca-app-pub-4762392528800851/6917375910";     //Gercek ID


        if (this.interstitialAdPvP != null)
            this.interstitialAdPvP.Destroy();

        this.interstitialAdPvP = new InterstitialAd(adUnitId);

        this.interstitialAdPvP.LoadAd(this.CreateAdRequest());

        this.interstitialAdPvP.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoadPvP;

        this.interstitialAdPvP.OnAdClosed += InterstitialAd_OnAdClosedPvP;
    }

    private void InterstitialAd_OnAdFailedToLoadPvP(object sender, AdFailedToLoadEventArgs e)
    {
        RequestIntertialPvP();
    }

    private void InterstitialAd_OnAdClosedPvP(object sender, EventArgs e)
    {
        PlayerPrefs.SetInt("PvPAdShowCount", 0);
        GameObject.FindObjectOfType<PvPAdManager>().pvpAdShowCount = 0;
        print("InstertialPVP closed");
        Invoke("ScaleTime", .05f);
        print("InstertialPvP time scaled 0");
    }

    public void ShowIntertialPvP()
    {
        if (this.interstitialAdPvP.IsLoaded())
        {
            interstitialAdPvP.Show();
            print("Instertial ad showed");
        }

        else
        {
            print("interstitialAd not loaded yet.");
        }
    }
    #endregion

    private void ScaleTime()
    {
        Time.timeScale = 0;
    }
}
