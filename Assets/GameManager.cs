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

        banner = new BannerAd("banner_top", BannerSize.BANNER, AdPosition.BOTTOM);
        //AdSize size = new AdSize(300, 250);
        //banner = new BannerAd("banner_top", size, AdPosition.BOTTOM);
        //banner = new BannerAd("banner_top", size, 0, 50);


        BilUnityJS.OnBannerImpression += OnBannerImpression;
        BilUnityJS.OnBannerFailure += OnBannerFailure;

        BilUnityJS.OnRewardedReady += OnRewardedReady;
        BilUnityJS.OnRewardedImpression += OnRewardedImpression;
        BilUnityJS.OnUserEarnedReward += OnUserEarnedReward;

        BilUnityJS.OnInterstitialReady += OnInterstitialReady;
        BilUnityJS.OnInterstitialImpression += OnInterstitialImpression;
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
        BilUnityJS.Instance.IsRewardedReady("coin");
    }
    public void PreloadRewardedAd()
    {
        BilUnityJS.Instance.PreLoadRewarded("coin");
    }
    public void PreloadRewardedAd2()
    {
        BilUnityJS.Instance.PreLoadRewarded("play");
    }
    public void ShowRewardedAd()
    {
        BilUnityJS.Instance.ShowRewarded("coin");
    }
    public void ShowRewardedAd2()
    {
        BilUnityJS.Instance.ShowRewarded("play");
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
        BilUnityJS.Instance.PreLoadInterstitial();
    }
    public void ShowInterstitialAd()
    {
        BilUnityJS.Instance.ShowInterstitial();
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
