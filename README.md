* Các bước tích hợp Ads vào Unity
- B1:
    Kéo AntGamesSDK.prefab vào trong Hierarchy.
- B2:
    Tạo folder "Plugins" trong Assets và copy file "AntGamesSDK.jslib" vào folder đó.
- B3: 
    Tích hợp code chạy demo trong file GameManager.cs


* Tích hợp Ads trong code C#:
1. Banner: init BannerAd trước khi show banner
    - Init: BannerAd banner = new BannerAd(slotID, position);
        + slotID: ID unique của banner tạo trên ant.game
        + position: vị trí hiển thị banner
            Default AdPosition.Top or AdPosition.Bottom Custom position
    - Show: banner.ShowAd();
    - Destroy: banner.Destroy();

    VD:
        BannerAd banner = new BannerAd("banner_top", AdPosition.BOTTOM);
        or
        BannerAd banner = new BannerAd("banner_top", 0, 50)
        banner.ShowBanner();
        banner.Destroy();

2. Reward:
    slotID: ID unique của reward tạo trên ant.game, dùng để preload và show theo slotID

    - Preload Ad trước khi show reward
        AntGamesSDK.Instance.PreloadRewardedAd(slotID);
            VD: AntGamesSDK.Instance.PreLoadRewarded(slotID);
    - Show: Bắt buộc phải preload trước khi show
        AntGamesSDK.Instance.ShowRewardedAd(slotID);
            VD: AntGamesSDK.Instance.ShowRewarded(slotID);

3. Interstitial:


4. SendEvent: Gửi Custom event
    VD:
        BannerData bannerData = new BannerData();
        bannerData.slotID = "banner1";
        EventData<BannerData> eventData = new EventData<BannerData>
        {
            eventName = "InitBanner",
            data = bannerData
        };
        AntGamesSDK.Instance.SendEvent(eventData);
