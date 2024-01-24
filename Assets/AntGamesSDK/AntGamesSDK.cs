using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Text.RegularExpressions;
public class AntGamesSDK : MonoBehaviour
{
    public static AntGamesSDK Instance;

    #region BannerAd
    public static Action<BannerData> OnBannerImpression;
    public static Action<BannerData> OnBannerFailure;
    #endregion

    #region RewardedAd
    public static Action<RewardedData> OnUserEarnedReward; // onRewarded
    public static Action<RewardedData> OnRewardedImpression; // onShowRewardedSuccess
    public static Action<RewardedData> OnRewardedReady; // onAdReady
    public static Action<RewardedData> OnRewardedClosed; // onClose
    public static Action<RewardedData> OnRewardedLoadFail; // onAdEmpty
    public static Action<RewardedData> OnRewardedShowFail; // onShowRewardedFail
    #endregion

    #region InterstitialAd
    public static Action<InterstitialData> OnInterstitialImpression;
    public static Action<InterstitialData> OnInterstitialReady;
    public static Action<InterstitialData> OnInterstitialClosed;
    public static Action<InterstitialData> OnInterstitialLoadFail;
    public static Action<InterstitialData> OnInterstitialShowFail;
    #endregion

    #region SendEvent
    public static Action<CustomData> OnSendEventSuccess;
    public static Action<CustomData> OnSendEventFail;
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
    private static extern void ShowBannerAd(string slotID, string position);
    [DllImport("__Internal")]
    private static extern void DestroyBannerAd(string slotID);

    [DllImport("__Internal")]
    private static extern void OnSendEvent(string option);

    void Awake()
    {
        if (AntGamesSDK.Instance == null)
            AntGamesSDK.Instance = this;
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
    internal void ShowBanner(string slotID, string position)
    {
        try
        {
            ShowBannerAd(slotID, position);
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("ShowBanner failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    internal void DestroyBanner(string slotID)
    {
        try
        {
            DestroyBannerAd(slotID);
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("DestroyBanner failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    void BannerAdCallback(string eventJSON)
    {
        EventData<BannerData> eventData = JsonUtility.FromJson<EventData<BannerData>>(eventJSON);
        if (eventData.eventName == "onShowBannerSuccess")
        {
            if (OnBannerImpression != null) OnBannerImpression(eventData.data);
            return;
        }
        if (eventData.eventName == "onShowBannerFail")
        {
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
        // Debug.Log("RewardedAdCallback: " + eventData.eventName);
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
    void InterstitialAdCallback(string eventJSON)
    {
        EventData<InterstitialData> eventData = JsonUtility.FromJson<EventData<InterstitialData>>(eventJSON);
        //Debug.Log("InterstitialAdCallback: " + eventData.eventName);
        //Debug.Log(eventData.data.rewardedType + " | " + eventData.data.data);

        if (eventData.eventName == "PreloadInterstitial")
        {
            return;
        }
        if (eventData.eventName == "onShowInterstitialSuccess")
        {
            if (OnInterstitialImpression != null) OnInterstitialImpression(eventData.data);
            return;
        }
        if (eventData.eventName == "onAdReady" || eventData.eventName == "IsInterstitialAdReady")
        {
            if (OnInterstitialReady != null) OnInterstitialReady(eventData.data);
            return;
        }
        if (eventData.eventName == "onClose")
        {
            if (OnInterstitialClosed != null) OnInterstitialClosed(eventData.data);
            return;
        }
        if (eventData.eventName == "onLoadInterstitialFail")
        {
            if (OnInterstitialLoadFail != null) OnInterstitialLoadFail(eventData.data);
            return;
        }
        if (eventData.eventName == "onShowInterstitialFail")
        {
            if (OnInterstitialShowFail != null) OnInterstitialShowFail(eventData.data);
            return;
        }
    }
    #endregion

    #region SendEvent
    internal void SendEvent<T>(EventData<T> eventData)
    {
        try
        {
            OnSendEvent(JsonUtility.ToJson(eventData));
        }
        catch (EntryPointNotFoundException e)
        {
            Debug.LogWarning("OnSendEvent failed. Make sure you are running a WebGL build in a browser:" + e.Message);
        }
    }
    void OnSendEventCallback(string eventJSON)
    {
        EventData<CustomData> eventData = JsonUtility.FromJson<EventData<CustomData>>(eventJSON);
        //Debug.Log("InterstitialAdCallback: " + eventData.eventName);
        //Debug.Log(eventData.data.rewardedType + " | " + eventData.data.data);

        if (eventData.eventName == "onSendEventSuccess")
        {
            if (OnSendEventSuccess != null) OnSendEventSuccess(eventData.data);
            return;
        }
        if (eventData.eventName == "onSendEventFail")
        {
            if (OnSendEventFail != null) OnSendEventFail(eventData.data);
            return;
        }
    }
    #endregion
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
public class InterstitialData
{
    public string data;
}
[Serializable]
public class BannerData
{
    public string slotID;
    public string data;
}
[Serializable]
public class CustomData
{
    public string eventName;
    public string data;
}

#region BannerAd Class 
public enum AdPosition
{
    TOP,
    BOTTOM
}
[Serializable]
public class BannerAd
{
    private string slotID;
    private string position;

    public BannerAd(string slotID, AdPosition position)
    {
        this.slotID = slotID;
        SetAdPosition(position);
    }

    public BannerAd(string slotID, int x, int y)
    {
        this.slotID = slotID;
        position = x + "_" + y;
    }

    private void SetAdPosition(AdPosition position)
    {
        if (position == AdPosition.TOP)
        {
            this.position = "top";
        }
        else if (position == AdPosition.BOTTOM)
        {
            this.position = "bottom";
        }
    }

    public void ShowAd()
    {
        AntGamesSDK.Instance.ShowBanner(slotID, position);
    }

    public void Destroy()
    {
        AntGamesSDK.Instance.DestroyBanner(slotID);
    }
}
#endregion