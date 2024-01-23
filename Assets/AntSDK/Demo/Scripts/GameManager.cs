using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Button btnShowInterAd;
    public Button btnShowRewardedAd;
    public Button btnShowBannerAd;


    void Awake()
    {
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

    public void ShowRewardedAd()
    {
        if (!AntGameAds.Instance) return;
        AntGameAds.Instance.ShowRewardedAd(OnRewardedEarned);
    }

    public void OnRewardedEarned()
    {
        Debug.Log("Rewarded Ad Completed");
    }
}
