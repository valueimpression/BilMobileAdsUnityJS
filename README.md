## Ant.games-SDK WebGL Unity3D

This repository contains the Ant.games SDK for WebGL Unity3D games. This allows you to display advertisements in the games published within the Ant.games network. [https://ant.games](https://ant.games)

## STEP 1:

[Download the plugin](https://drive.google.com/file/d/1eBUVgoMVhz-B_QU0JaCEkJaWqQZUm3EC/) and Import the .unitypackage into your game.

Download here: [https://drive.google.com/file/d/1eBUVgoMVhz-B_QU0JaCEkJaWqQZUm3EC/](https://drive.google.com/file/d/1eBUVgoMVhz-B_QU0JaCEkJaWqQZUm3EC/)

## STEP 2:

Open the Resources Folder and find the Object named **“BilUnityJS”**, then replace the Ads ID values with your own keys.

![](https://33333.cdn.cke-cs.com/kSW7V9NHUXugvhoQeFaf/images/13b16ad97f0f9664b70e0d03381044a2e229881a9441b08f.png)

## STEP 3:

Use AntGameAds.Instance.ShowInterstitialAd() to show an interstitial ad

Use AntGameAds.Instance.ShowRewardedAd() to show an Rewarded ad

#### NOTE: Use PreLoad function before calling show advertisement

## STEP 4:

Make use of the events AntGameAds.OnResumeGame and AntGameAds.OnPauseGame for resuming/pausing your game in between ads.

## Example:

```plaintext
public class ExampleClass: MonoBehaviour {
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

    public void RemoveCallbacks()
    {
        if (!AntGameAds.Instance) return;
        AntGameAds.Instance.OnResumeGame -= ResumeGame;
        AntGameAds.Instance.OnPauseGame -= PauseGame;
    }

    private void OnDestroy()
    {
        RemoveCallbacks();
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
```
