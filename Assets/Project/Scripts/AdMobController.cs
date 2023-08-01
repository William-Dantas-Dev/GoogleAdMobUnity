using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using TMPro;

public class AdMobController : MonoBehaviour
{
    public static AdMobController instance;
    private string appId = "ca-app-pub-3940256099942544/6300978111";
    private BannerView bannerAD;
    private InterstitialAd interstitialAd;

    public TextMeshProUGUI txtInfo;
    public bool publishingApp = false;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            Debug.Log("Started?");
        });
    }


    void Update()
    {

    }

    public void RequestBanner()
    {
        if(bannerAD == null)
        {
            string bannerId = "";
            if (publishingApp)
            {
                bannerId = "Valid Key";
            }
            else
            {
                // Test Key
                bannerId = "ca-app-pub-3940256099942544/6300978111";
            }

            AdSize adSize = new AdSize(320, 100);
            bannerAD = new BannerView(bannerId, adSize, AdPosition.Bottom);
            if (publishingApp)
            {
                AdRequest adRequest = new AdRequest.Builder().Build();
                bannerAD.LoadAd(adRequest);
            }
            else
            {
                AdRequest adRequest = new AdRequest.Builder().AddKeyword("2077ef9a63d2b398840261c8221a0c9b").Build();
                bannerAD.LoadAd(adRequest);
            }
        }
        
    }

    public void RemoveBanner()
    {
        if(bannerAD != null)
        {
            bannerAD.Destroy();
            bannerAD = null;
        }
    }

    public void RequestInterstitial()
    {
        string interstitialId = "";
        if (publishingApp)
        {
            interstitialId = "Valid Key";
        }
        else
        {
            // Test Key
            interstitialId = "ca-app-pub-3940256099942544/1033173712";
        }

        interstitialAd = new InterstitialAd(interstitialId);

        interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitialAd.OnAdOpening += HandleOnAdOpening;
        interstitialAd.OnAdClosed += HandleOnAdClosed;

        if (publishingApp)
        {
            AdRequest adRequest = new AdRequest.Builder().Build();
            interstitialAd.LoadAd(adRequest);
        }
        else
        {
            AdRequest adRequest = new AdRequest.Builder().AddKeyword("2077ef9a63d2b398840261c8221a0c9b").Build();
            interstitialAd.LoadAd(adRequest);
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs e)
    {
        interstitialAd.OnAdLoaded -= HandleOnAdLoaded;
        interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        interstitialAd.OnAdOpening -= HandleOnAdOpening;
        interstitialAd.OnAdClosed -= HandleOnAdClosed;

        interstitialAd.Destroy();
        txtInfo.text = "Anúncio Fechado!";
    }

    public void HandleOnAdOpening(object sender, EventArgs e)
    {
        txtInfo.text = "Anúncio Aberto!";
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        txtInfo.text = "Falha ao Carregar Anúncio! Erro: " + e.ToString();
        RequestInterstitial();
    }

    public void HandleOnAdLoaded(object sender, EventArgs e)
    {
        txtInfo.text = "Anúncio Foi Carregado!";
        ShowInterstitial();
    }

    public void ShowInterstitial()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
}
