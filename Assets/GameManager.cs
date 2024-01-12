using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text textToShow;

    void Awake()
    {
        BilUnityJS.OnBannerImpression += OnBannerImpression;
        BilUnityJS.OnBannerFailure += OnBannerFailure;

        BilUnityJS.OnRewardedReady += OnRewardedReady;
        BilUnityJS.OnRewardedImpression += OnRewardedImpression;
        BilUnityJS.OnUserEarnedReward += OnUserEarnedReward;

        BilUnityJS.OnInterstitialReady += OnInterstitialReady;
        BilUnityJS.OnInterstitialImpression += OnInterstitialImpression;
        BilUnityJS.OnInterstitialFailure += OnInterstitialFailure;
    }


    #region BannerAD
    public void ShowBannerAd()
    {
        BilUnityJS.Instance.ShowBanner("ad_banner_container", "300x250");
    }
    public void ShowBannerAd2()
    {
        BilUnityJS.Instance.ShowBanner("ad_banner_container2", "728x90");
    }
    private void OnBannerImpression(BannerData data)
    {
        Debug.Log("OnBannerImpression");
    }
    private void OnBannerFailure(BannerData data)
    {
        Debug.Log("OnBannerFailure");
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
        Debug.Log("Interstitial Loaded: " + BilUnityJS.Instance.IsInterstitialReady());
    }
    public void PreloadInterstitialAd()
    {
        BilUnityJS.Instance.PreLoadInterstitial();
    }
    public void ShowInterstitialAd()
    {
        BilUnityJS.Instance.ShowInterstitial();
    }

    private void OnInterstitialReady(int isLoaded)
    {
        string str = isLoaded == 1 ? "IsReady" : "NotReady";
        Debug.Log("OnInterstitialReady: " + str);
        textToShow.text = "OnInterstitialReady: " + str;
    }
    private void OnInterstitialImpression()
    {
        Debug.Log("OnInterstitialImpression");
        textToShow.text = "OnInterstitialImpression";
    }
    private void OnInterstitialFailure()
    {
        Debug.Log("OnInterstitialFailure");
        textToShow.text = "OnInterstitialFailure";
    }

    #endregion
}
