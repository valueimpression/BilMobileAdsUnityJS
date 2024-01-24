using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text textToShow;
    BannerAd banner;

    void Awake()
    {

        banner = new BannerAd("banner_top", AdPosition.BOTTOM);
        //banner = new BannerAd("banner_top", 0, 50);

        AntGamesSDK.OnBannerImpression += OnBannerImpression;
        AntGamesSDK.OnBannerFailure += OnBannerFailure;

        AntGamesSDK.OnRewardedReady += OnRewardedReady;
        AntGamesSDK.OnRewardedImpression += OnRewardedImpression;
        AntGamesSDK.OnUserEarnedReward += OnUserEarnedReward;

        AntGamesSDK.OnInterstitialReady += OnInterstitialReady;
        AntGamesSDK.OnInterstitialImpression += OnInterstitialImpression;

        BannerData bannerData = new BannerData();
        bannerData.slotID = "banner1";
        EventData<BannerData> eventData = new EventData<BannerData>
        {
            eventName = "InitBanner",
            data = bannerData
        };
        AntGamesSDK.Instance.SendEvent(eventData);

        AntGamesSDK.OnSendEventSuccess += OnSendEventSuccess;
        AntGamesSDK.OnSendEventFail += OnSendEventFail;
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
    public void ShowBannerAd()
    {
        if (banner != null) banner.ShowAd();
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
        AntGamesSDK.Instance.PreLoadRewarded("coin");
    }
    public void PreloadRewardedAd2()
    {
        AntGamesSDK.Instance.PreLoadRewarded("play");
    }
    public void ShowRewardedAd()
    {
        AntGamesSDK.Instance.ShowRewarded("coin");
    }
    public void ShowRewardedAd2()
    {
        AntGamesSDK.Instance.ShowRewarded("play");
    }

    private void OnUserEarnedReward(RewardedData obj)
    {
        Debug.Log("OnUserEarnedReward: " + obj.rewardedType);
        Debug.Log(obj.data);
        textToShow.text = "OnUserEarnedReward: " + obj.rewardedType;
    }
    private void OnRewardedReady(RewardedData obj)
    {
        string mess = "OnRewardedReady: " + obj.rewardedType + " | " + obj.data;
        Debug.Log(mess);
        textToShow.text = "OnRewardedReady: " + mess;
    }
    private void OnRewardedImpression(RewardedData obj)
    {
        Debug.Log("OnRewardedImpression: " + obj.rewardedType);
        textToShow.text = "OnRewardedImpression: " + obj.rewardedType;
    }
    private void OnRewardedFailure()
    {
        Debug.Log("OnRewardedFailure");
        textToShow.text = "OnRewardedFailure";
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
        textToShow.text = "OnInterstitialReady: " + obj.data;
    }
    private void OnInterstitialImpression(InterstitialData obj)
    {
        Debug.Log("OnInterstitialImpression: " + obj.data);
        textToShow.text = "OnInterstitialImpression: " + obj.data;
    }
    private void OnInterstitialFailure()
    {
        Debug.Log("OnInterstitialFailure");
        textToShow.text = "OnInterstitialFailure";
    }

    #endregion
}
