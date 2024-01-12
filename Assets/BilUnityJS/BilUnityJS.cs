using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Text.RegularExpressions;
public class BilUnityJS : MonoBehaviour
{
    public static BilUnityJS Instance;

    // const string BANNER_AD = "BannerAd";
    // const string REWARDED_AD = "RewardedAd";
    // const string INTERSTITIAL_AD = "InterstitialAd";

    #region BannerAd
    private bool _isBannerLoaded = false;
    public static Action<BannerData> OnBannerImpression;
    public static Action<BannerData> OnBannerFailure;
    #endregion

    #region RewardedAd
    //private bool _isRewardedReady = false;
    public static Action<RewardedData> OnUserEarnedReward; // onRewarded
    public static Action<RewardedData> OnRewardedImpression; // onShowRewardedSuccess
    public static Action<RewardedData> OnRewardedReady; // onAdReady
    public static Action<RewardedData> OnRewardedClosed; // onClose
    public static Action<RewardedData> OnRewardedLoadFail; // onAdEmpty
    public static Action<RewardedData> OnRewardedShowFail; // onShowRewardedFail
    #endregion

    #region InterstitialAd
    private bool _isInterstitialReady = false;
    public static Action OnInterstitialImpression;
    public static Action OnInterstitialFailure;
    public static Action<int> OnInterstitialReady;
    #endregion

    [DllImport("__Internal")]
    private static extern void InitSDK();

    [DllImport("__Internal")]
    private static extern void IsRewardedAdReady(string rewardedType);
    [DllImport("__Internal")]
    private static extern void PreLoadRewardedAd(string rewardedType);
    [DllImport("__Internal")]
    private static extern void ShowRewardedAd(string rewardedType);

    [DllImport("__Internal")]
    private static extern void PreLoadInterstitialAd();
    [DllImport("__Internal")]
    private static extern void ShowInterstitialAd();

    [DllImport("__Internal")]
    // private static extern void ShowBannerAd(params object[] objects);
    private static extern void ShowBannerAd(string elemID, string adSize);

    [DllImport("__Internal")]
    private static extern void OnSendEvent(string options);

    void Awake()
    {
        if (BilUnityJS.Instance == null)
            BilUnityJS.Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);

        Init();
    }

    void Init()
    {
        try
        {
            InitSDK();
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("initialization failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }

    #region BannerAd
    // params object[] objects
    internal void ShowBanner(string elemID, string adSize)
    {
        try
        {
            ShowBannerAd(elemID, adSize);
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("ShowBanner failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    void BannerAdCallback(string eventJSON)
    {
        EventData<BannerData> eventData = JsonUtility.FromJson<EventData<BannerData>>(eventJSON);
        if (eventData.eventName == "onShowBannerSuccess")
        {
            _isBannerLoaded = true;
            if (OnBannerImpression != null) OnBannerImpression(eventData.data);
            return;
        }
        if (eventData.eventName == "onShowBannerFail")
        {
            _isBannerLoaded = false;
            if (OnBannerFailure != null) OnBannerFailure(eventData.data);
            return;
        }
    }
    #endregion

    #region RewardedAd
    internal void PreLoadRewarded(string rewardedType)
    {
        try
        {
            PreLoadRewardedAd(rewardedType);
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("PreLoadRewardedAd failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    internal void ShowRewarded(string rewardedType)
    {
        try
        {
            ShowRewardedAd(rewardedType);
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("ShowRewardedAd failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    internal void IsRewardedReady(string rewardedType)
    {
        try
        {
            IsRewardedAdReady(rewardedType);
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("ShowRewardedAd failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    void RewardedAdCallback(string eventJSON)
    {
        EventData<RewardedData> eventData = JsonUtility.FromJson<EventData<RewardedData>>(eventJSON);
        Debug.Log("RewardedAdCallback: " + eventData.eventName);
        //Debug.Log(eventData.data.rewardedType + " | " + eventData.data.data);

        if (eventData.eventName == "PreloadRewarded")
        {
            return;
        }
        if (eventData.eventName == "onRewarded" || eventData.eventName == "onComplete")
        {
            if (OnUserEarnedReward != null) OnUserEarnedReward(eventData.data);
            return;
        }
        if (eventData.eventName == "onShowRewardedSuccess")
        {
            if (OnRewardedImpression != null) OnRewardedImpression(eventData.data);
            return;
        }
        if (eventData.eventName == "onAdReady" || eventData.eventName == "IsRewardedAdReady")
        {
            if (OnRewardedReady != null) OnRewardedReady(eventData.data);
            return;
        }
        if (eventData.eventName == "onClose")
        {
            if (OnRewardedClosed != null) OnRewardedClosed(eventData.data);
            return;
        }
        if (eventData.eventName == "onAdEmpty")
        {
            if (OnRewardedLoadFail != null) OnRewardedLoadFail(eventData.data);
            return;
        }
        if (eventData.eventName == "onShowRewardedFail")
        {
            if (OnRewardedShowFail != null) OnRewardedShowFail(eventData.data);
            return;
        }
    }
    #endregion

    #region InterstitialAd
    internal void PreLoadInterstitial()
    {
        try
        {
            PreLoadInterstitialAd();
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("PreLoadInterstitialAd failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    internal void ShowInterstitial()
    {
        try
        {
            ShowInterstitialAd();
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("ShowInterstitialAd failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    public bool IsInterstitialReady()
    {
        return _isInterstitialReady;
    }
    void PreloadInterstitialCallback(int loaded)
    {
        _isInterstitialReady = (loaded == 1);

        if (OnInterstitialReady != null) OnInterstitialReady(loaded);
    }
    void InterstitialImpressionCallback()
    {
        _isInterstitialReady = false;

        if (OnInterstitialImpression != null) OnInterstitialImpression();
    }
    void InterstitialFailureCallback()
    {
        _isInterstitialReady = false;

        if (OnInterstitialFailure != null) OnInterstitialFailure();
    }
    #endregion

    internal void SendEvent(string options)
    {
        try
        {
            OnSendEvent(options);
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("OnSendEvent failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
}

[Serializable]
public class EventData<T>
{
    public T data;
    public string eventName;
}

[Serializable]
public class RewardedData
{
    public string rewardedType;
    public string data;
}

[Serializable]
public class BannerData
{
    public string elementId;
    public string adSize;
}