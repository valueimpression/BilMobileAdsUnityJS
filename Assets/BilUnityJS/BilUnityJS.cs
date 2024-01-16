using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Text.RegularExpressions;
public class BilUnityJS : MonoBehaviour
{
    public static BilUnityJS Instance;

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
    private static extern void ShowBannerAd(string slotID, string adSize, string position);
    [DllImport("__Internal")]
    private static extern void DestroyBannerAd(string slotID);

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
    internal void ShowBanner(string slotID, string adSize, string position)
    {
        try
        {
            ShowBannerAd(slotID, adSize, position);
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
        Debug.Log("InterstitialAdCallback: " + eventData.eventName);
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
    void PreloadInterstitialCallback(int loaded)
    {
        //_isInterstitialReady = (loaded == 1);

        //if (OnInterstitialReady != null) OnInterstitialReady(loaded);
    }
    void InterstitialImpressionCallback()
    {
        //_isInterstitialReady = false;

        //if (OnInterstitialImpression != null) OnInterstitialImpression();
    }
    void InterstitialFailureCallback()
    {
        //_isInterstitialReady = false;

        //if (OnInterstitialLoadFail != null) OnInterstitialLoadFail();
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



public enum AdPosition
{
    TOP,
    BOTTOM
}
public enum BannerSize
{
    BANNER,
    LARGE_BANNER,
    MEDIUM_RECTANGLE,
    FULL_BANNER,
    LEADERBOARD
}
[Serializable]
public class AdSize
{
    private int width;
    private int height;

    public AdSize(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public string GetSize()
    {
        return width + "x" + height;
    }
}
[Serializable]
public class BannerAd
{
    private string slotID;
    private string adSize;
    private string position;

    public BannerAd(string slotID, BannerSize adSize, AdPosition position)
    {
        this.slotID = slotID;
        SetAdSize(adSize);
        SetAdPosition(position);
    }

    public BannerAd(string slotID, BannerSize adSize, int x, int y)
    {
        this.slotID = slotID;
        SetAdSize(adSize);
        position = x + "_" + y;
    }

    public BannerAd(string slotID, AdSize adSize, AdPosition position)
    {
        this.slotID = slotID;
        this.adSize = adSize.GetSize();
        SetAdPosition(position);
    }


    public BannerAd(string slotID, AdSize adSize, int x, int y)
    {
        this.slotID = slotID;
        this.adSize = adSize.GetSize();
        position = x + "_" + y;
    }

    private void SetAdSize(BannerSize adSize)
    {
        if (adSize == BannerSize.BANNER)
        {
            this.adSize = "320x50";
        }
        else if (adSize == BannerSize.LARGE_BANNER)
        {
            this.adSize = "320x100";
        }
        else if (adSize == BannerSize.MEDIUM_RECTANGLE)
        {
            this.adSize = "300x250";
        }
        else if (adSize == BannerSize.FULL_BANNER)
        {
            this.adSize = "468x60";
        }
        else if (adSize == BannerSize.LEADERBOARD)
        {
            this.adSize = "728x90";
        }
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
        BilUnityJS.Instance.ShowBanner(slotID, adSize, position);
    }

    public void Destroy()
    {
        BilUnityJS.Instance.DestroyBanner(slotID);
    }
}