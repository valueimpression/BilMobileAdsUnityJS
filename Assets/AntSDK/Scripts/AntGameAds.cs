using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntGameAds : Singleton<AntGameAds>
{
    BannerAd banner;

    private Action _rewardSuccessCallback;
    private Action _rewardFailCallback;


    public Action OnResumeGame;
    public Action OnPauseGame;

    public string rewardAdID;
    public string bannerAdID;
    public string interstitialAdID;


    public override void Awake()
    {
        base.Awake();
        //if (Application.isMobilePlatform)
        //{
        //    banner = new BannerAd(mobileBannerID, AdPosition.BOTTOM);
        //    Debug.Log("Load Banner Mobile");
        //}
        //else
        //{
        //    banner = new BannerAd(pcBannerID, AdPosition.BOTTOM);
        //    Debug.Log("Load Banner Desktop");
        //}
        //banner = new BannerAd("banner_top", 0, 50);

        AdConfig();

        AntGamesSDK.OnBannerImpression += OnBannerImpression;
        AntGamesSDK.OnBannerFailure += OnBannerFailure;

        AntGamesSDK.OnRewardedReady += OnRewardedReady;
        AntGamesSDK.OnRewardedImpression += OnRewardedImpression;
        AntGamesSDK.OnUserEarnedReward += OnUserEarnedReward;
        AntGamesSDK.OnRewardedClosed += OnRewardClosed;

        AntGamesSDK.OnInterstitialReady += OnInterstitialReady;
        AntGamesSDK.OnInterstitialImpression += OnInterstitialImpression;
        AntGamesSDK.OnInterstitialClosed += OnInterstitialClosed;

        AntGamesSDK.OnSendEventSuccess += OnSendEventSuccess;
        AntGamesSDK.OnSendEventFail += OnSendEventFail;

        LoadBannerAds();

    }

    private void AdConfig()
    {
        if (String.IsNullOrEmpty((rewardAdID))) Debug.LogWarning("Rewarded Ad ID is empty");
        if (String.IsNullOrEmpty((interstitialAdID))) Debug.LogWarning("Interstitial Ad ID is empty");
        if (String.IsNullOrEmpty((bannerAdID))) Debug.LogWarning("Banner Ad ID is empty");
    }

    private void Start()
    {
        BannerData bannerData = new BannerData();
        bannerData.slotID = bannerAdID;
        EventData<BannerData> eventData = new EventData<BannerData>
        {
            eventName = "InitBanner",
            data = bannerData
        };
        AntGamesSDK.Instance.SendEvent(eventData);

        PreloadRewardedAd();
        PreloadInterstitialAd();
        ShowBannerAd();
    }

    private void OnSendEventSuccess(CustomData obj)
    {
        Debug.Log("OnSendEventSuccess: " + obj.eventName + " | " + obj.data);
    }
    private void OnSendEventFail(CustomData obj)
    {
        Debug.Log("OnSendEventFail: " + obj.eventName + " | " + obj.data);
    }

    #region BannerAD

    public void LoadBannerAds()
    {
        banner = new BannerAd(bannerAdID, AdPosition.BOTTOM);
        Debug.Log("Load Banner Mobile");
    }

    public void ShowBannerAd()
    {
        if (banner != null)
        {
            Debug.Log("Banner Ads Ready");
            banner.ShowAd();
        }
        else
            Debug.Log("Banner Ads Load Error");
    }
    public void DestroyBannerAd()
    {
        if (banner != null) banner.Destroy();
    }
    private void OnBannerImpression(BannerData obj)
    {
        Debug.Log("OnBannerImpression: " + obj.slotID);
        Debug.Log("Data: " + obj.data);
    }
    private void OnBannerFailure(BannerData obj)
    {
        Debug.Log("OnBannerFailure: " + obj.slotID);
        Debug.Log("Data: " + obj.data);
    }
    #endregion

    #region RewardedAD
    public void IsRewardedReady()
    {
        AntGamesSDK.Instance.IsRewardedReady("coin");
    }
    public void PreloadRewardedAd()
    {
        Debug.Log("PreloadRewardedAd: " + rewardAdID);
        AntGamesSDK.Instance.PreLoadRewarded(rewardAdID);
    }
    
    public void ShowRewardedAd(Action successCallback, Action failCallback = null)
    {
        _rewardSuccessCallback = successCallback;
        _rewardFailCallback = failCallback;
        AntGamesSDK.Instance.ShowRewarded(rewardAdID);
    }

    private void OnUserEarnedReward(RewardedData obj)
    {
        _rewardSuccessCallback?.Invoke();
        if (OnResumeGame != null) OnResumeGame();
        Debug.Log("OnUserEarnedReward: " + obj.rewardedType);
        Debug.Log(obj.data);
        //textToShow.text = "OnUserEarnedReward: " + obj.rewardedType;
    }
    private void OnRewardedReady(RewardedData obj)
    {
        string mess = "OnRewardedReady: " + obj.rewardedType + " | " + obj.data;
        Debug.Log(mess);
        //textToShow.text = "OnRewardedReady: " + mess;
    }
    private void OnRewardedImpression(RewardedData obj)
    {
        if (OnPauseGame != null) OnPauseGame();
        Debug.Log("OnRewardedImpression: " + obj.rewardedType);
        //textToShow.text = "OnRewardedImpression: " + obj.rewardedType;
    }
    private void OnRewardedFailure()
    {
        Debug.Log("OnRewardedFailure");
        //textToShow.text = "OnRewardedFailure";
    }

    void OnRewardClosed(RewardedData obj)
    {
        StartCoroutine(PreloadRewardCoroutine());
        if (OnResumeGame != null) OnResumeGame();
    }

    IEnumerator PreloadRewardCoroutine()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        PreloadRewardedAd();
    }

    #endregion

    #region InterstitialAD
    public void IsInterstitialReady()
    {
        Debug.Log("Interstitial Loaded: ");
    }
    public void PreloadInterstitialAd()
    {
        AntGamesSDK.Instance.PreLoadInterstitial();
    }
    public void ShowInterstitialAd()
    {
        AntGamesSDK.Instance.ShowInterstitial();
    }

    private void OnInterstitialReady(InterstitialData obj)
    {
        Debug.Log("OnInterstitialReady: " + obj.data);
        //textToShow.text = "OnInterstitialReady: " + obj.data;
    }
    private void OnInterstitialImpression(InterstitialData obj)
    {
        if (OnPauseGame != null) OnPauseGame();
        Debug.Log("OnInterstitialImpression: " + obj.data);
        //textToShow.text = "OnInterstitialImpression: " + obj.data;
    }
    private void OnInterstitialFailure()
    {
        Debug.Log("OnInterstitialFailure");
        if (OnResumeGame != null) OnResumeGame();
        //textToShow.text = "OnInterstitialFailure";
    }

    private void OnInterstitialClosed(InterstitialData obj)
    {
        Debug.Log("Interstitial Ad Closed");
        if (OnResumeGame != null) OnResumeGame();
    }

    #endregion
}
