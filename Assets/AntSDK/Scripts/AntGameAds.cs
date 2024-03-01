using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntGameAds : Singleton<AntGameAds>
{
    BannerAd banner;

    private Action _rewardSuccessCallback;
    private Action _rewardFailCallback;

    public bool isShowDefaultBottomBanner;

    public Action OnResumeGame;
    public Action OnPauseGame;

    public string rewardAdID;
    public string bannerAdID;
    public string interstitialAdID;

    public string ratingURL;

    private bool _isRewardReady = false;
    private bool _isIntersAdReady = false;
    
    public override void Awake()
    {
        base.Awake();

        AdConfig();

        AntGamesSDK.OnBannerImpression += OnBannerImpression;
        AntGamesSDK.OnBannerFailure += OnBannerFailure;

        AntGamesSDK.OnRewardedReady += OnRewardedReady;
        AntGamesSDK.OnRewardedImpression += OnRewardedImpression;
        AntGamesSDK.OnUserEarnedReward += OnUserEarnedReward;
        AntGamesSDK.OnRewardedClosed += OnRewardClosed;
        AntGamesSDK.OnRewardedShowFail += OnRewardedFailure;
        AntGamesSDK.OnRewardedLoadFail += OnRewardedLoadFail;

        AntGamesSDK.OnInterstitialReady += OnInterstitialReady;
        AntGamesSDK.OnInterstitialImpression += OnInterstitialImpression;
        AntGamesSDK.OnInterstitialClosed += OnInterstitialClosed;
        AntGamesSDK.OnInterstitialShowFail += OnInterstitialFailure;

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
        if(isShowDefaultBottomBanner) ShowBannerAd();
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
    public bool IsRewardedReady()
    {
        return _isRewardReady;
    }
    public void PreloadRewardedAd()
    {
        Debug.Log("PreloadRewardedAd: " + rewardAdID);
        AntGamesSDK.Instance.PreLoadRewarded(rewardAdID);
    }
    
    public void ShowRewardedAd(Action successCallback, Action failCallback = null)
    {
        if (!_isRewardReady)
        {
            Debug.Log("RewardedAd Show Failed! Error: No Ad!!!");
            return;
        }
        _rewardSuccessCallback = successCallback;
        _rewardFailCallback = failCallback;
        AntGamesSDK.Instance.ShowRewarded(rewardAdID);
    }

    private void OnUserEarnedReward(RewardedData obj)
    {
        _rewardSuccessCallback?.Invoke();
        _rewardSuccessCallback = null;
        //if (OnResumeGame != null) OnResumeGame();
        Debug.Log("OnUserEarnedReward: " + obj.rewardedType);
        Debug.Log(obj.data);
        //textToShow.text = "OnUserEarnedReward: " + obj.rewardedType;
    }
    private void OnRewardedReady(RewardedData obj)
    {
        string mess = "OnRewardedReady: " + obj.rewardedType + " | " + obj.data;
        Debug.Log(mess);
        //textToShow.text = "OnRewardedReady: " + mess;
        _isRewardReady = true;
    }
    private void OnRewardedImpression(RewardedData obj)
    {
        if (OnPauseGame != null) OnPauseGame();
        Debug.Log("OnRewardedImpression: " + obj.rewardedType);
        //textToShow.text = "OnRewardedImpression: " + obj.rewardedType;
    }
    private void OnRewardedFailure(RewardedData data)
    {
        _rewardSuccessCallback = null;
        _rewardFailCallback?.Invoke();
        _rewardFailCallback = null;
        if (OnResumeGame != null) OnResumeGame();
        Debug.Log("OnRewardedFailure");
        _isRewardReady = false;
        //textToShow.text = "OnRewardedFailure";
    }

    private void OnRewardedLoadFail(RewardedData obj)
    {
        string mess = "OnRewardedLoadFail: " + obj.rewardedType + " | " + obj.data;
        Debug.Log(mess);
        _isRewardReady = false;
    }

    void OnRewardClosed(RewardedData obj)
    {
        StartCoroutine(PreloadRewardCoroutine());
        _rewardSuccessCallback = null;
        _rewardFailCallback = null;
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
        _isIntersAdReady = true;
    }
    public void PreloadInterstitialAd()
    {
        Debug.Log("PreloadInterstitialAd: ");
        _isIntersAdReady = false;
        AntGamesSDK.Instance.PreLoadInterstitial();
    }
    public void ShowInterstitialAd()
    {
        AntGamesSDK.Instance.ShowInterstitial();
    }

    private void OnInterstitialReady(InterstitialData obj)
    {
        _isIntersAdReady = true;
        Debug.Log("OnInterstitialReady: " + obj.data);
        //textToShow.text = "OnInterstitialReady: " + obj.data;
    }
    private void OnInterstitialImpression(InterstitialData obj)
    {
        if (OnPauseGame != null) OnPauseGame();
        Debug.Log("OnInterstitialImpression: " + obj.data);
        //textToShow.text = "OnInterstitialImpression: " + obj.data;
    }
    private void OnInterstitialFailure(InterstitialData data)
    {
        _isIntersAdReady = false;
        Debug.Log("OnInterstitialFailure");
        if (OnResumeGame != null) OnResumeGame();
        //textToShow.text = "OnInterstitialFailure";
    }

    private void OnInterstitialClosed(InterstitialData obj)
    {
        _isIntersAdReady = false;
        Debug.Log("Interstitial Ad Closed");
        if (OnResumeGame != null) OnResumeGame();
    }

    #endregion
}
