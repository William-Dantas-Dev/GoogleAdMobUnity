using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdMobController : MonoBehaviour
{
    public static AdMobController instance;
    private string appId = "ca-app-pub-3940256099942544/6300978111";
    private BannerView bannerAD;
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
        bannerAD = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Bottom);
        if(publishingApp)
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

    public void RemoveBanner()
    {
        bannerAD.Destroy();
    }
}
