using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HadesAdManager : Singleton<HadesAdManager>
{
    //public Action OnRewardEarned;

    public override void Awake()
    {
        base.Awake();
        InitCallbacks();
    }

    public void InitCallbacks()
    {
        if (!AntGameAds.Instance) return;
        AntGameAds.Instance.OnResumeGame += ResumeGame;
        AntGameAds.Instance.OnPauseGame += PauseGame;
    }

    public void ResumeGame()
    {
        // RESUME MY GAME
        Debug.Log("RESUME GAME");
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        // PAUSE MY GAME
        Debug.Log("PAUSE GAME");
        Time.timeScale = 0.01f;
        AudioListener.volume = 0f;
    }


    public void PreLoadInterstitialAd()
    {
        if (!AntGameAds.Instance) return;
        AntGameAds.Instance.PreloadInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        if (!AntGameAds.Instance) return;
        AntGameAds.Instance.ShowInterstitialAd();
    }

    public void PreLoadRewardedAd()
    {
        if (!AntGameAds.Instance) return;
        AntGameAds.Instance.PreloadRewardedAd();
    }

    public void ShowRewardedAd(Action successCallback)
    {
        if (!AntGameAds.Instance) return;
        AntGameAds.Instance.ShowRewardedAd(successCallback);
    }

    public bool IsRewardedReady()
    {
        return AntGameAds.Instance.IsRewardedReady();
    }

    public void ShowBanner()
    {
        AntGameAds.Instance.ShowBannerAd();
    }

    
}

