using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using TMPro;
public class GoogleMobileAd : MonoBehaviour
{
    private static BannerView bannerView;
    private static RewardBasedVideoAd rewardBasedVideoAd;
    private static InterstitialAd interstitial;
    [SerializeField]
    private AudioSource coinSound;
    
  
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        BuildIntAdInstance();
        BuildRewardedVideoInstance();
        BuildBannerInstance();
        RequestRewardedVideo();
        RequestInterstitial();
    }

    private void BuildIntAdInstance()
    {
        if (interstitial == null)
        {
            string adUnitId = "ca-app-pub-7905586974013978/5775785027";
            interstitial = new InterstitialAd(adUnitId);
            interstitial.OnAdLoaded += HandleOnIntAdLoaded;
            interstitial.OnAdFailedToLoad += HandleOnIntAdFailedToLoad;
            interstitial.OnAdOpening += HandleOnIntAdOpened;
            interstitial.OnAdClosed += HandleOnIntAdClosed;
            interstitial.OnAdLeavingApplication += HandleOnIntAdLeavingApplication;
        }
    }

    private void BuildBannerInstance()
    {
        if (bannerView == null)
        {
            string adUnitId = "ca-app-pub-7905586974013978/6158928402";
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            bannerView.OnAdLoaded += HandleBannerAdLoaded;
            bannerView.OnAdFailedToLoad += HandleBannerAdFailedToLoad;
            bannerView.OnAdOpening += HandleAdBannerOpened;
            bannerView.OnAdClosed += HandleAdBannerClosed;
            bannerView.OnAdLeavingApplication += HandleAdBannerLeftApplication;
        }
    }

    private void BuildRewardedVideoInstance()
    {
        if (rewardBasedVideoAd == null)
        {
            rewardBasedVideoAd = RewardBasedVideoAd.Instance;
            rewardBasedVideoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
            rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            rewardBasedVideoAd.OnAdOpening += HandleRewardBasedVideoOpened;
            rewardBasedVideoAd.OnAdStarted += HandleRewardBasedVideoStarted;
            rewardBasedVideoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewardBasedVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;
            rewardBasedVideoAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        }
    }

    private void RequestBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }


    private void RequestRewardedVideo()
    {
        string adUnitId = "ca-app-pub-7905586974013978/9523458341";
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideoAd.LoadAd(request,adUnitId);
    }

    private void RequestInterstitial()
    {
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
    {
        //SetText("Video Closed Before it ends");
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        RequestRewardedVideo();
    }

    internal void RemoveInterAd()
    {
        if (interstitial != null)
        {
            interstitial.OnAdLoaded -= HandleOnIntAdLoaded;
            interstitial.OnAdFailedToLoad -= HandleOnIntAdFailedToLoad;
            interstitial.OnAdOpening -= HandleOnIntAdOpened;
            interstitial.OnAdClosed -= HandleOnIntAdClosed;
            interstitial.OnAdLeavingApplication -= HandleOnIntAdLeavingApplication;
            interstitial.Destroy();
        }
    }

    internal void RemoveRewardedVideoAd()
    {
        rewardBasedVideoAd.OnAdLoaded -= HandleRewardBasedVideoLoaded;
        rewardBasedVideoAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideoAd.OnAdOpening -= HandleRewardBasedVideoOpened;
        rewardBasedVideoAd.OnAdStarted -= HandleRewardBasedVideoStarted;
        rewardBasedVideoAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedVideoAd.OnAdClosed -= HandleRewardBasedVideoClosed;
        rewardBasedVideoAd.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
    }

    private void HandleRewardBasedVideoRewarded(object sender, Reward e)
    {
        GameStateHolder.totalCoinCount = GameStateHolder.totalCoinCount + 100;
        coinSound.Play();
    }

    
    private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
    {
       
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
    {
       
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestRewardedVideo();
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
    {
        
    }

    public bool CheckRewardedVideoLoaded()
    {
        return rewardBasedVideoAd.IsLoaded();
    }

    public void ShowRewardedVideo()
    {
        if (rewardBasedVideoAd.IsLoaded())
            rewardBasedVideoAd.Show();    
    }
    
    public void ShowIntertrialAd()
    {
        if (interstitial.IsLoaded())
           interstitial.Show();
    }

    private void HandleOnIntAdLoaded(object sender, EventArgs e)
    {
        
    }

    private void HandleOnIntAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestInterstitial();
    }

    private void HandleOnIntAdOpened(object sender, EventArgs e)
    {
       
    }

    private void HandleOnIntAdClosed(object sender, EventArgs e)
    {
        
    }

    private void HandleOnIntAdLeavingApplication(object sender, EventArgs e)
    {
       
    }

    
    private void HandleAdBannerLeftApplication(object sender, EventArgs e)
    {
       
    }

    private void HandleAdBannerClosed(object sender, EventArgs e)
    {
        
    }

    private void HandleAdBannerOpened(object sender, EventArgs e)
    {
       
    }

    private void HandleBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestBanner();
    }

    private void HandleBannerAdLoaded(object sender, EventArgs e)
    {
        
    }

    public void ShowBannerAd()
    {
        RequestBanner();
    }

    public void RemoveBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
            bannerView.Destroy();
        }
    }

    

}
